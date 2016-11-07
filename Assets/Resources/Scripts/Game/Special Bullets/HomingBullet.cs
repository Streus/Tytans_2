using UnityEngine;
using System.Collections;

public class HomingBullet : Bullet {

	public GameObject homingTarget;

	public override void LateUpdate()
	{
		base.LateUpdate ();

		//home in to target
		if (homingTarget != null)
		{
			Vector2 point = homingTarget.transform.position;
			Quaternion rot = Quaternion.LookRotation(transform.position - new Vector3(point.x, point.y, -100f), Vector3.back);
			transform.rotation = rot;
			transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
			transform.GetComponent<Rigidbody2D> ().velocity *= 0;
			transform.GetComponent<Rigidbody2D> ().AddForce (transform.up * -speed, ForceMode2D.Impulse);
		}
	}
}
