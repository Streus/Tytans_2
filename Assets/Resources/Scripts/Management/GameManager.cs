using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// The one game-manager
	public static GameManager manager;

	// The player object
	public static GameObject player;

	// Use this for initialization
	void Start () {
		// ensure there's only one GameManager
		if(manager == null){
			DontDestroyOnLoad (transform.gameObject);
			manager = this;
		}
		else if(manager != this){
			Destroy(transform.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
