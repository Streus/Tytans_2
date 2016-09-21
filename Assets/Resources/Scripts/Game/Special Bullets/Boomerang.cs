using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	Bullet bullScript;
	float cycle;

	// Use this for initialization
	void Start () {
		bullScript = transform.GetComponent<Bullet>();
		cycle = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (cycle >= 0.5f){
			transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			transform.GetComponent<Rigidbody2D> ().AddForce (transform.up * bullScript.speed, ForceMode2D.Impulse);
		}
		cycle += Time.deltaTime;
	}
}
