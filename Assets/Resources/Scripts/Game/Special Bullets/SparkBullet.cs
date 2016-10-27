using UnityEngine;
using System.Collections;

public class SparkBullet : Bullet {

	protected override void hitEffect (Collider2D col)
	{
		Entity other = col.transform.GetComponent<Entity>();
		if (other != null)
			other.addStatus (new StatusShocked (5f, col.transform, 0.5f));
	}
}
