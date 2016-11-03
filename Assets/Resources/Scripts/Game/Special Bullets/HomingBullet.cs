using UnityEngine;
using System.Collections;

public class HomingBullet : Bullet {

	public GameObject homingTarget;

	public override void LateUpdate()
	{
		base.LateUpdate ();

		//TODO figure out homing behavior
		transform.GetComponent<Rigidbody2D> ().AddForce (transform.up * speed);
	}
}
