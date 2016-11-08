using UnityEngine;
using System.Collections;

public class CirclingBullet : Bullet
{

	public override void LateUpdate ()
	{
		base.LateUpdate ();

		if (creator != null)
			transform.position = creator.transform.position;
		else
			die ();

		transform.localRotation = Quaternion.Euler (0f, 0f, transform.localRotation.eulerAngles.z + 1);
	}
}
