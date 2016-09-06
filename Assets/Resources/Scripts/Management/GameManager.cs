using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// The one game-manager
	public static GameManager manager;

	// The player object and supporting vars
	public static GameObject player;
	private PlayerClass playerClass;
	private GameObject playerBullet;

	// Options to save for this game file
	private string saveName;
	private Difficulty difficulty;
	private string lastScene;
	private Vector2 spawnCoordinates;

	// Other misc variables
	private bool paused;
	private Menu[] menus;

	// Use this for initialization
	void Start () {
		//instantiate defaults
		saveName = "default";
		lastScene = "Overworld";
		spawnCoordinates = Vector2.zero;

		paused = false;
		menus = null;

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
		if (Input.GetKeyDown (Bindings.pause)) {
			paused = !paused;
			pause ();
			//TODO toggle pause menu
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
		//save this room as the last entered room
		lastScene = arg0.name;

		//gather a list of all the menus in this room
		menus = FindObjectsOfType<Menu> ();

		//make player
		player = (GameObject)Instantiate((GameObject)Resources.Load("Prefabs/Entities/Players/Player", typeof(GameObject)), (Vector3)(spawnCoordinates), Quaternion.identity);
		Entity entScr = player.transform.GetComponent<Entity>();
		Player plyScr = player.transform.GetComponent<Player>();

		//set up stats depending on class
		switch(playerClass){
		case PlayerClass.defender:
			plyScr.bullet = playerBullet;
			plyScr.myClass = playerClass;
			entScr.healthMax = 150f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 1.25f;
			entScr.energyMax = 100f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 0.25f;
			entScr.speed = 27;
			entScr.addAbility(new FlakShot(player.transform, playerBullet), 0);
			entScr.addAbility(new AbsorptionField(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerDefender", typeof(Sprite));
			break;
		case PlayerClass.skirmisher:
			plyScr.bullet = playerBullet;
			plyScr.myClass = playerClass;
			entScr.healthMax = 75f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 0.5f;
			entScr.energyMax = 100f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 0.25f;
			entScr.speed = 50;
			entScr.addAbility(new BurstShot(player.transform, playerBullet), 0);
			entScr.addAbility(new Dash(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerSkirmisher", typeof(Sprite));
			break;
		case PlayerClass.caster:
			plyScr.bullet = playerBullet;
			plyScr.myClass = playerClass;
			entScr.healthMax = 50f;
			entScr.health = entScr.healthMax;
			entScr.healthRegen = 0.5f;
			entScr.energyMax = 200f;
			entScr.energy = entScr.energyMax;
			entScr.energyRegen = 1f;
			entScr.speed = 35;
			entScr.addAbility(new RailgunShot(player.transform, playerBullet), 0);
			entScr.addAbility(new CoreOverload(player.transform), 1);

			player.transform.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Sprites/Game/Entities/PlayerCaster", typeof(Sprite));
			break;
		}
	}

	public void setDifficulty(Difficulty d){
		difficulty = Options.difficulty;
	}

	public void setPlayerClass(PlayerClass pc){
		playerClass = pc;
	}

	public void setBullet(GameObject bullet){
		playerBullet = bullet;
	}

	// Get and set saveGame
	public void setSaveName(string svnm){
		saveName = svnm;
	}
	public string getSaveName(){
		return saveName;
	}
}
