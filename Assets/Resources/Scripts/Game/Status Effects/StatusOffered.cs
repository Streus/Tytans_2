using UnityEngine;
using System.Collections;

public class StatusOffered : StatusEffect 
{
	private GameObject effect;
	private GameObject healTarget;
	private float healAmount;

	public StatusOffered(float dur, Transform e, GameObject healtar) : base(dur, e)
	{
		name = "Offered";
		desc = "Chosen for sacrifice.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectOffered");

		healTarget = healtar;
		healAmount = invokerVars.health;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusOffered (initDuration, e, healTarget);
	}

	public override void apply ()
	{
		//apply effect
		//TODO add Offered effect
	}

	public override void revert ()
	{
		//remove effect


		//create homing heal bullet of destiny
		if (invokerVars.health <= 0)
		{
			GameObject bul = Bullet.createBullet (invoker.gameObject, Resources.Load<GameObject> ("Prefabs/Bullets/BulletHomingHealth"), invoker.position, invoker.rotation);
			HomingBullet hb = bul.GetComponent<HomingBullet> ();
			hb.homingTarget = healTarget;
			hb.damage = -healAmount;
		}
	}
}
