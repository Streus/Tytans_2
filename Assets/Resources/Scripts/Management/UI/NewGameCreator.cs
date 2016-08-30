using UnityEngine;
using System.Collections;

public class NewGameCreator : MonoBehaviour {

	public GameObject gameManager;

	// Misc game options
	private Difficulty difficulty = Difficulty.Easy;
	private bool tutorial = Options.tutorial;

	// Player options
	private PlayerClass pClass = PlayerClass.defender;
	private float healthMax = 0;
	private float healthRegen = 0;
	private float energyMax = 0;
	private float energyRegen = 0;
	private int speed = 0;
	private string classAbility = "Dash";
	private string shootAbility = "BasicShot";
	private GameObject bulletType;
	private Sprite shipSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public enum PlayerClass
{
	defender, skirmisher, caster
}