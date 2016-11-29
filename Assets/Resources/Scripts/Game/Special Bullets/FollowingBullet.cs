using UnityEngine;
using System.Collections;

public class FollowingBullet : Bullet {

	public override void LateUpdate ()
	{
		base.LateUpdate ();

		if (creator != null)
			transform.position = creator.transform.position;
		else
			die ();
	}
}
