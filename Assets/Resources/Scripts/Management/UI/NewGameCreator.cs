using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class NewGameCreator : MonoBehaviour {

	public GameObject gameManager;

	// Misc game options
	private Difficulty difficulty = Difficulty.Easy;
	private bool tutorial = Options.tutorial;

	// Player options
	private PlayerClass pClass = PlayerClass.defender;
	private Sprite shipSprite;
	private float healthMax = 0;
	private float healthRegen = 0;
	private float energyMax = 0;
	private float energyRegen = 0;
	private int speed = 0;
	private string classAbility = "Dash";
	private string shootAbility = "BasicShot";
	private GameObject bulletType;

	// Important children
	Transform classGroup;
	Text classDescription;
	Transform bulletGroup;
	Slider difficultySlider;
	Text difficultyText;

	// Use this for initialization
	void Start () {
		classGroup = transform.GetChild(0).GetChild(2);
		classDescription = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
		bulletGroup = transform.GetChild(0).GetChild(7);
		difficultySlider = transform.GetChild(0).GetChild(5).GetChild(0).GetComponent<Slider>();
		difficultyText = transform.GetChild(0).GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>();

		setClass(0);
		setDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// All setters
	public void setClass(int pc)
	{
		pClass = (PlayerClass)pc;
		switch(pClass){
		case PlayerClass.defender:
			classDescription.text = 
				"Defender.  Slow but sturdy front-line ship that thrives when under fire.";
			break;
		case PlayerClass.skirmisher:
			classDescription.text = 
				"Skirmisher.  Fast and agile fighter that survives by avoiding hits.";
			break;
		case PlayerClass.caster:
			classDescription.text = 
				"Obliterator.  Advanced craft that, while fragile, has absurd destructive capability.";
			break;
		}
	}

	public GameObject getBullet() { return bulletType; }
	public void setBullet(GameObject x) { bulletType = x; }
		
	public void setDifficulty()
	{
		difficulty = (Difficulty)(int)difficultySlider.value;
		string difftext = "";
		switch (difficulty) {
		case Difficulty.Easy:
			difftext = "Easy";
			difficultyText.color = Color.green;
			break;
		case Difficulty.Normal:
			difftext = "Normal";
			difficultyText.color = Color.yellow;
			break;
		case Difficulty.Hard: 
			difftext = "Hard";
			difficultyText.color = Color.red;
			break;
		}
		difficultyText.text = difftext;
	}

	public void moveToGame()
	{
		//TODO set up GameManager
		EditorSceneManager.LoadScene ("Overworld");
	}
} 

public enum PlayerClass
{
	defender, skirmisher, caster
}