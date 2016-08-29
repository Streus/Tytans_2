using UnityEngine;
using System.Collections;

public class OpeningMenu : MonoBehaviour {

	public float dispDuration;
	public MenuManager manager;
	public Menu transitionMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dispDuration -= Time.deltaTime;
		if (dispDuration <= 0f) {
			manager.showMenu (transitionMenu);
			Destroy (transform.gameObject);
		}
	}
}
