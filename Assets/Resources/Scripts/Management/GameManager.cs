using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour {

	// The one game-manager
	public static GameManager manager;

	// The player object and supporting vars
	public static GameObject player;
	public PlayerClass playerClass;
	public string playerBullet;

	// Options to save for this game file
	public string saveName;
	public Difficulty difficulty;
	public Vector3 spawnCoordinates;
	public Ability[] flexAbilities = new Ability[3];
	public ArrayList learnedAbilites;
	public bool[] completedBosses = new bool[14];

	// Other misc variables
	private bool paused;
	public static CameraControl cameraController;

	// Use this for initialization
	void Start () {
		//instantiate defaults
		saveName = "";
		spawnCoordinates = Vector3.zero;
		learnedAbilites = new ArrayList();
		player = null;

		paused = false;

		//ensure there's only one GameManager
		if(manager == null){
			DontDestroyOnLoad (transform.gameObject);
			manager = this;
		}
		else if(manager != this){
			Destroy(transform.gameObject);
		}

		//listen for scene changes
		SceneManager.sceneLoaded += EditorSceneManager_sceneLoaded;
	}

	// Update is called once per frame
	void Update () {
		//toggle the pause state of the game and toggle a pause menu
		if (Input.GetKeyDown (Bindings.pause)) 
		{
			paused = !paused;
			pause ();

			if (paused)
				MenuManager.menuSystem.showMenu (MenuManager.menuSystem.getMenu("Pause"));
			else
				MenuManager.menuSystem.showMenu (MenuManager.menuSystem.getMenu("Empty"));
		}
	}

	// Stop simulation on all Rigidbodies
	public void pause()
	{
		Rigidbody2D[] bodies = FindObjectsOfType<Rigidbody2D> ();
		foreach (Rigidbody2D body in bodies) {
			body.simulated = !paused;
		}
	}

	// Open the death screen
	public void playerDeath()
	{
		//bring up death screen
		MenuManager.menuSystem.showMenu(MenuManager.menuSystem.getMenu("Death"));

		//roll a new taunt
		MenuManager.menuSystem.getMenu ("Death").transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<CyclingDeathTaunt> ().rollNewTaunt ();
	}

	// Reload the Overworld scene
	public void restartGame()
	{
		SceneManager.UnloadScene("Overworld");
		SceneManager.LoadScene ("Overworld");
	}

	void EditorSceneManager_sceneLoaded (UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
	{
		if(arg0.name == "MainMenu" || player != null)
			return;

		//make player
		player = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs/Entities/Player", typeof(GameObject)), (Vector3)(spawnCoordinates), Quaternion.identity);
		Entity entScr = player.transform.GetComponent<Entity>();
		Player plyScr = player.transform.GetComponent<Player>();

		//set up stats and default abilites depending on class
		switch(playerClass){
		case PlayerClass.defender:
			plyScr.bullet = Resources.Load<GameObject>("Prefabs/Bullets/" + playerBullet);
			plyScr.myClass = playerClass;
			entScr.healthMax = 150f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 1.25f;
			entScr.energyMax = 100f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 1.5f;
			entScr.speed = 27;
			entScr.addAbility(new FlakShot(player.transform, plyScr.bullet), 0);
			entScr.addAbility(new AbsorptionField(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Game/Entities/PlayerDefender");
			break;
		case PlayerClass.skirmisher:
			plyScr.bullet = Resources.Load<GameObject>("Prefabs/Bullets/" + playerBullet);
			plyScr.myClass = playerClass;
			entScr.healthMax = 75f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 0.5f;
			entScr.energyMax = 100f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 1.25f;
			entScr.speed = 45;
			entScr.addAbility(new BurstShot(player.transform, plyScr.bullet), 0);
			entScr.addAbility(new Dash(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Game/Entities/PlayerSkirmisher");
			break;
		case PlayerClass.caster:
			plyScr.bullet = Resources.Load<GameObject>("Prefabs/Bullets/" + playerBullet);
			plyScr.myClass = playerClass;
			entScr.healthMax = 50f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 0.5f;
			entScr.energyMax = 200f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 2f;
			entScr.speed = 35;
			entScr.addAbility(new RailgunShot(player.transform, plyScr.bullet), 0);
			entScr.addAbility(new CoreOverload(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Game/Entities/PlayerCaster");
			break;
		}

		//add flexAbilities
		for(int i = 0; i < 3; i++)
		{
			if (flexAbilities [i] == null)
				continue;
			flexAbilities [i].invoker = player.transform;
			if (flexAbilities [i] is BulletFlexAbility)
				((BulletFlexAbility)flexAbilities [i]).bulletPrefab = plyScr.bullet;
			entScr.addAbility(flexAbilities[i].Copy(), i + 2);
		}

		//add learned abilities
		plyScr.learnedAbilities = (ArrayList)learnedAbilites.Clone();
		for (int i = 0; i < plyScr.learnedAbilities.Count; i++) {
			((Ability)learnedAbilites [i]).invoker = player.transform;
		}
	}

	// Get and set saveGame
	public void setSaveName(string svnm){
		saveName = svnm;
	}
	public string getSaveName(){
		return saveName;
	}

	// Create a .dat file named (saveName) and save game relevent information to it
	public void saveGame()
	{
		//set up data container
		SaveGameFile save = new SaveGameFile();

		//save stuff
		save.save_playerClass = (int)playerClass;
		save.save_playerBullet = playerBullet = player.transform.GetComponent<Player>().bullet.name;
		save.save_difficulty = (int)difficulty;
		save.save_spawnX = spawnCoordinates.x;
		save.save_spawnY = spawnCoordinates.y;

		//save abilities
		save.abilityNames = new string[3];
		Entity entscr = player.transform.GetComponent<Entity> ();
		for (int i = 0; i < 3; i++) {
			if (entscr.abilities [i + 2] == null)
				save.abilityNames [i] = "";
			else
				save.abilityNames [i] = entscr.abilities [i + 2].GetType().AssemblyQualifiedName;
		}

		//save learned abilites
		Ability[] temp = new Ability[player.transform.GetComponent<Player>().learnedAbilities.Count];
		player.transform.GetComponent<Player> ().learnedAbilities.CopyTo (temp);
		save.learnedAbilityNames = new string[temp.Length];
		for (int i = 0; i < temp.Length; i++) {
			save.learnedAbilityNames [i] = temp [i].GetType().AssemblyQualifiedName;
		}

		//save defeated bosses
		save.defeatedBosses = completedBosses;

		//serialize and save
		FileStream file = File.Open(Application.persistentDataPath + "\\" + saveName + ".dat", FileMode.Create);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, save);
		file.Close();
	}

	// Load (saveName).dat and begin a game with its data
	public void loadGame()
	{
		if(File.Exists(Application.persistentDataPath + "\\" + saveName + ".dat")){
			//load and deserialize file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "\\" + saveName + ".dat", FileMode.Open);
			SaveGameFile save = (SaveGameFile)bf.Deserialize(file);

			//set GameManager values to match deserialized values
			playerClass = (PlayerClass)save.save_playerClass;
			playerBullet = save.save_playerBullet;
			difficulty = (Difficulty)save.save_difficulty;
			spawnCoordinates.x = save.save_spawnX;
			spawnCoordinates.y = save.save_spawnY;

			//load abilities
			for (int i = 0; i < save.abilityNames.Length; i++) {
				if (save.abilityNames [i] == "")
					flexAbilities [i] = null;
				else
					flexAbilities [i] = (Ability)Activator.CreateInstance (Type.GetType (save.abilityNames [i]));
			}

			//load learned abilites
			for (int i = 0; i < save.learnedAbilityNames.Length; i++) {
				learnedAbilites.Add((Ability)Activator.CreateInstance (Type.GetType (save.learnedAbilityNames [i])));
			}

			//load defeated bosses
			completedBosses = save.defeatedBosses;
		}
	}

	[Serializable]
	private class SaveGameFile : ISerializable
	{
		public int save_playerClass;
		public string save_playerBullet;
		public int save_difficulty;

		//spawn position
		public float save_spawnX;
		public float save_spawnY;

		//abilities;
		public string[] abilityNames;

		//learned abilities
		public string[] learnedAbilityNames;

		//defeated bosses
		public bool[] defeatedBosses;

		public SaveGameFile() { }

		//for deserialization
		public SaveGameFile(SerializationInfo info, StreamingContext context)
		{
			save_playerClass = (int) info.GetValue("playerClass", typeof(int));
			save_playerBullet = (string) info.GetValue("playerBullet", typeof(string));
			save_difficulty = (int) info.GetValue("difficulty", typeof(int));

			save_spawnX = (float) info.GetValue("spawnX", typeof(float));
			save_spawnY = (float) info.GetValue("spawnY", typeof(float));

			//abilityNames
			abilityNames = new string[(int)info.GetValue("abilityNamesLength", typeof(int))];
			for(int i = 0; i < abilityNames.Length; i++){
				abilityNames[i] = (string)info.GetValue("abilityNames" + i, typeof(string));
			}

			//learnedAbilityNames
			learnedAbilityNames = new string[(int)info.GetValue("learnedAbilityNamesLength", typeof(int))];
			for(int i = 0; i < learnedAbilityNames.Length; i++){
				learnedAbilityNames[i] = (string)info.GetValue("learnedAbilityNames" + i, typeof(string));
			}

			//deafeatedBosses
			defeatedBosses = new bool[(int)info.GetValue("defeatedBossesLength", typeof(int))];
			for(int i = 0; i < defeatedBosses.Length; i++){
				defeatedBosses[i] = (bool)info.GetValue("defeatedBosses" + i, typeof(bool));
			}
		}

		//for serialization
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("playerClass", save_playerClass);
			info.AddValue ("playerBullet", save_playerBullet);
			info.AddValue ("difficulty", save_difficulty);

			info.AddValue ("spawnX", save_spawnX);
			info.AddValue ("spawnY", save_spawnY);

			//abilityNames
			for (int i = 0; i < abilityNames.Length; i++) {
				info.AddValue ("abilityNames" + i, abilityNames [i], typeof(string));
			}
			info.AddValue ("abilityNamesLength", abilityNames.Length, typeof(int));

			//learnedAbilityNames
			for (int i = 0; i < learnedAbilityNames.Length; i++) {
				info.AddValue ("learnedAbilityNames" + i, learnedAbilityNames [i], typeof(string));
			}
			info.AddValue ("learnedAbilityNamesLength", learnedAbilityNames.Length, typeof(int));

			//defeatedBosses
			for (int i = 0; i < defeatedBosses.Length; i++) {
				info.AddValue ("defeatedBosses" + i, defeatedBosses [i], typeof(bool));
			}
			info.AddValue ("defeatedBossesLength", defeatedBosses.Length, typeof(int));
		}
	}
}