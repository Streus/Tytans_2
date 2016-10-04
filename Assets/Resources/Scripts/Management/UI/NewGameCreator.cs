using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;

public class NewGameCreator : MonoBehaviour {

	// Misc game options
	private Difficulty difficulty = Difficulty.Easy;
	private string saveName;

	// Player options
	private PlayerClass pClass = PlayerClass.defender;
	private string bulletType;

	// Important children
	//Transform classGroup;
	Text classDescription;
	//Transform bulletGroup;
	Slider difficultySlider;
	Text difficultyText;
	InputField gameNameInput;

	// Use this for initialization
	void Start () {
		//classGroup = transform.GetChild(0).GetChild(2);
		classDescription = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
		//bulletGroup = transform.GetChild(0).GetChild(5);
		difficultySlider = transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Slider>();
		difficultyText = transform.GetChild(1).GetChild(4).GetChild(1).GetChild(0).GetComponent<Text>();
		gameNameInput = transform.GetChild (1).GetChild (2).GetComponent<InputField> ();

		setSaveName ();
		setClass(0);
		setDifficulty();
		setBullet("BulletBasic");

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

	public string getBullet() { return bulletType; }
	public void setBullet(string x) { bulletType = x; }

	public void setSaveName() 
	{
		Match newName = Regex.Match (gameNameInput.text, "[A-Za-z0-9]+", RegexOptions.Singleline);
		if (newName.Value.Equals(gameNameInput.text))
			saveName = newName.Value;
		else{
			gameNameInput.text = "";
			gameNameInput.ForceLabelUpdate ();
		}
	}
		
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

	// Assign chosen values to GameManager.manager and move to game scene
	public void moveToGame()
	{
		//set up GameManager (clearing any possible loaded values)
		GameManager.manager.flexAbilities = new Ability[3];
		GameManager.manager.learnedAbilites.Clear ();
		GameManager.manager.completedBosses = new bool[14];
		GameManager.manager.setSaveName(saveName);
		GameManager.manager.setDifficulty(difficulty);
		GameManager.manager.setPlayerClass(pClass);
		GameManager.manager.setBullet(bulletType);
		EditorSceneManager.LoadScene ("Overworld");
	}
} 

public enum PlayerClass
{
	defender, skirmisher, caster
}