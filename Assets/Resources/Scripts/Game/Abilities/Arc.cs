using UnityEngine;
using System.Collections;

public class Arc : Ability
{
	private float teleportDist;

	public Arc(Transform e) : base(e){ }
	public Arc() : base(){ }

	protected override void setValues ()
	{
		dispName = "Arc";
		desc = "Teleport to a position and damage\nall enemies passed through.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityArc");
		cost = 15f;
		cooldown = 6f;
		currentCD = cooldown;

		teleportDist = 7f;
	}

	public override Ability Copy ()
	{
		return new Arc (invoker);
	}

	public override void use ()
	{
		Vector3 targetPos = invoker.position + invoker.up * -teleportDist;
		RaycastHit2D pathCheck = Physics2D.Raycast (invoker.position, -invoker.up, teleportDist, 1 << 9);
		Collider2D targetCheck = Physics2D.OverlapCircle (targetPos, invoker.GetComponent<CircleCollider2D> ().radius, 1 << 9);
		if (pathCheck.collider == null && targetCheck == null)
		{
			base.use ();

			//deal damage
			RaycastHit2D[] hitCheck = Physics2D.CircleCastAll(invoker.position, 0.5f, -invoker.up, teleportDist, 1 << 8);
			for (int i = 0; i < hitCheck.Length; i++)
			{
				Bullet.dealDamage (hitCheck [i].collider.GetComponent<Entity> (), 10f);
			}

			//teleport
			GameObject portal = Resources.Load<GameObject> ("Prefabs/Effects/ArcPortalEffect");

			GameObject portInst = (GameObject)MonoBehaviour.Instantiate (portal, invoker.position, invoker.rotation);
			MonoBehaviour.Destroy (portInst, 1f);

			invoker.position = targetPos;

			Quaternion invRot = Quaternion.Euler (0, 0, portInst.transform.rotation.eulerAngles.z - 180);
			portInst = (GameObject)MonoBehaviour.Instantiate (portal, invoker.position, invRot);
			MonoBehaviour.Destroy (portInst, 1f);
		}
	}
}
