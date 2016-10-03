using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class LoadGameList : MonoBehaviour {

	// Populate list of games from A.PDP
	void Start () {
		//load all .dat files from A.PDP
		string[] saveGames = Directory.GetFiles (Application.persistentDataPath, "*.dat");

		//create buttons to correspond to each .dat file
		for (int i = 0; i < saveGames.Length; i++) 
		{
			Debug.Log (saveGames [i]);//DEBUG CODE
			Instantiate (Resources.Load<GameObject> ("Prefabs/UI/SecondaryButton"), transform, false); //TODO create button specific to loading games
			RectTransform listBox = transform.GetComponent<RectTransform>();
			listBox.sizeDelta = new Vector2 (listBox.sizeDelta.x, listBox.sizeDelta.y + 40f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
