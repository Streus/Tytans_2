using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

	// The one game-manager
	public static GameManager manager;

	// The player object and supporting vars
	public static GameObject player;
	private PlayerClass playerClass;
	private string playerBullet;

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

			Menu[] menuList = MenuManager.menuSystem.getMenus ();
			foreach (Menu menu in menuList) 
			{
				if (paused && menu.gameObject.name == "Pause")
					MenuManager.menuSystem.showMenu (menu);
				else if (!paused && menu.gameObject.name == "Empty")
					MenuManager.menuSystem.showMenu (menu);
			}
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

	void EditorSceneManager_sceneLoaded (UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
	{
		//make player
		player = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs/Entities/Players/Player", typeof(GameObject)), (Vector3)(spawnCoordinates), Quaternion.identity);
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
			entScr.energyRegen = 1.25f;
			entScr.speed = 27;
			entScr.addAbility(new FlakShot(player.transform, plyScr.bullet), 0);
			entScr.addAbility(new AbsorptionField(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerDefender", typeof(Sprite));
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
			entScr.speed = 50;
			entScr.addAbility(new BurstShot(player.transform, plyScr.bullet), 0);
			entScr.addAbility(new Dash(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerSkirmisher", typeof(Sprite));
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

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerCaster", typeof(Sprite));
			break;
		}

		//add flexAbilities
		for(int i = 0; i < 3; i++)
		{
			entScr.addAbility(flexAbilities[i], i + 2);
		}

		//add learned abilities
		plyScr.learnedAbilities = (ArrayList)learnedAbilites.Clone();
	}

	public void setDifficulty(Difficulty d){
		difficulty = Options.difficulty;
	}

	public void setPlayerClass(PlayerClass pc){
		playerClass = pc;
	}

	public void setBullet(string bullet){
		playerBullet = bullet;
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
		save.save_playerClass = (int)playerClass;
		save.save_playerBullet = playerBullet;
		save.save_difficulty = (int)difficulty;
		save.save_spawnX = spawnCoordinates.x;
		save.save_spawnY = spawnCoordinates.y;
		//save abilities
		Debug.Log("Cannot save abilities yet.");

		//save learned abilites
		Debug.Log("Cannot save learned abilities yet.");

		//save defeated bosses
		Debug.Log("Cannot save defeated bosses yet.");

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
			Debug.Log("Cannot load abilities yet.");

			//load learned abilites
			Debug.Log("Cannot load learned abilities yet.");

			//load defeated bosses
			Debug.Log("Cannot load defeated bosses yet.");
		}
	}

	[Serializable]
	private class SaveGameFile
	{
		public int save_playerClass;
		public string save_playerBullet;
		public int save_difficulty;

		//spawn position
		public float save_spawnX;
		public float save_spawnY;

		//abilities;


		//learned abilities


		//defeated bosses

	}
}