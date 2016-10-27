using UnityEngine;
using System.Collections;

public class BoomerangBullet : Bullet {

	float cycle;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		cycle = 0f;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (cycle >= 0.5f){
			transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			transform.GetComponent<Rigidbody2D> ().AddForce (transform.up * speed, ForceMode2D.Impulse);
		}
		cycle += Time.deltaTime;
	}
}
