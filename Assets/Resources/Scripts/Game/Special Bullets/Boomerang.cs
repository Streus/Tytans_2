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
		if(cycle >= 1f)
			bullScript.speed = -bullScript.speed;
		cycle += Time.deltaTime;
	}
}
