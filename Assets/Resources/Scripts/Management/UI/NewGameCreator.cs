﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class NewGameCreator : MonoBehaviour {

	// Misc game options
	private Difficulty difficulty = Difficulty.Easy;

	// Player options
	private PlayerClass pClass = PlayerClass.defender;
	private string bulletType;

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
		//set up GameManager
		GameManager.manager.setDifficulty(difficulty);
		GameManager.manager.setPlayerClass(pClass);
		GameManager.manager.setBullet(bulletType);
		GameManager.manager.setSaveName("New Game");
		EditorSceneManager.LoadScene ("Overworld");
	}
} 

public enum PlayerClass
{
	defender, skirmisher, caster
}