using UnityEngine;
using System.Collections;

public class BasicShot : BulletFlexAbility {

	public BasicShot(Transform e, GameObject bt) : base(e, bt){ }
	public BasicShot() : base(){ }

	protected override void setValues ()
	{
		dispName = "Shoot";
		desc = "Fire a bullet.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityBasicShot", typeof(Sprite));
		cost = -3f;
		cooldown = 0.5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new BasicShot (invoker, bulletPrefab);
	}

	public override void use()
	{
		base.use ();

		Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, invoker.rotation);
	}
}
