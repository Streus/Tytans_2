using UnityEngine;
using System.Collections;

public class BossSpawnTrigger : MonoBehaviour {

	public GameObject boss;
	public bool armed;

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (boss != null && col.gameObject == GameManager.player && armed) {
			//TODO boss intro animation

			//activate boss
			boss.SetActive (true);
			armed = false;
		}
	}
}
