using UnityEngine;
using System.Collections;

public class RallyBullet : Bullet {

	protected override void hitEffect (Collider2D col)
	{
		Entity other = col.transform.GetComponent<Entity> ();
		if (other != null) {
			other.addStatus (new StatusRallied (10f, col.transform, 3f, 10));
			createHitText (col.transform.position, Color.white, "Rallied!");
		}
	}
}
