using UnityEngine;
using System.Collections;

public class StatusSharingBullet : Bullet {

	protected override void hitEffect (Collider2D col)
	{
		Entity other = col.transform.GetComponent<Entity>();
		if (other != null) 
		{
			ArrayList shared = creator.GetComponent<Entity> ().statuses;
			foreach (object status in shared) 
			{
				other.addStatus (((StatusEffect)status).Copy (other.transform));
			}
		}
	}
}
