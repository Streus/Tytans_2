using UnityEngine;
using System.Collections;

public class FlameBullet : Bullet {

	protected override void hitEffect (Collider2D col)
	{
		Entity other = col.transform.GetComponent<Entity> ();
		if (other != null && Random.value < 0.33f) {
			other.addStatus (new StatusFire (10f, col.transform, 1f));
			createHitText (col.transform.position, Color.white, "Burning!");
		}
	}
}
