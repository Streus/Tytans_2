using UnityEngine;
using System.Collections;

public class ChargeBullet : FollowingBullet
{
	protected override void hitEffect (Collider2D col)
	{
		if (col.gameObject != null)
		{
			Entity other = col.transform.GetComponent<Entity> ();
			other.addStatus (new StatusStunned(2f, col.transform));
		}
	}
}
