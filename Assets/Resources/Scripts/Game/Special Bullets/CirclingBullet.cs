using UnityEngine;
using System.Collections;

public class CirclingBullet : FollowingBullet
{
	public override void LateUpdate ()
	{
		base.LateUpdate ();

		transform.localRotation = Quaternion.Euler (0f, 0f, transform.localRotation.eulerAngles.z + 1);
	}
}
