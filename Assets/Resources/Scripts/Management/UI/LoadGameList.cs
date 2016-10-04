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
			Debug.Log("Loaded " + saveGames[i]);
			GameObject element = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/LoadGameElement"), transform, false);
			element.GetComponent<LoadGameElement>().setGameName(saveGames[i]);
			RectTransform listBox = transform.GetComponent<RectTransform>();
			listBox.sizeDelta = new Vector2 (listBox.sizeDelta.x, listBox.sizeDelta.y + 95f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
