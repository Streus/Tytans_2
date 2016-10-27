using UnityEngine;
using System.Collections;

public class VerdictBullet : Bullet {

	protected override void hitEffect (Collider2D col)
	{
		Entity other = col.transform.GetComponent<Entity> ();
		if (other != null) 
		{
			float r = Random.value;
			if (r <= 0.5f)
				other.addStatus (new StatusGuilty (7f, col.transform, 5f));
			else
				other.addStatus (new StatusInnocent (7f, col.transform, 1f));
		}
	}
}
