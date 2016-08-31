using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	// Use this for initialization
	void Start () {
		classGroup = transform.GetChild(0).GetChild(2);
		classDescription = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
		bulletGroup = transform.GetChild(0).GetChild(7);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// All setters
	public void setDifficulty(int x) { difficulty = (Difficulty)x; }

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
		
	} 

public enum PlayerClass
{
	defender, skirmisher, caster
}