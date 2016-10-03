using UnityEngine;
using System.Collections;

public class SavePoint : MonoBehaviour {

	public bool canSave;

	// Use this for initialization
	void Awake () {
		enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		//rotation effect
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 1));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject == GameManager.player && canSave)
		{
			Entity playervars = col.GetComponent<Entity>();

			//clean up the player for saving
			playervars.addStatus(new StatusCleansed(5f, col.transform));
			playervars.health = playervars.healthMax;
			playervars.energy = playervars.energyMax;
			playervars.shieldHealth = playervars.shieldMax;

			//update spawn coordinates and save
			GameManager.manager.spawnCoordinates = transform.position;
			GameManager.manager.saveGame();
		}
	}
}
