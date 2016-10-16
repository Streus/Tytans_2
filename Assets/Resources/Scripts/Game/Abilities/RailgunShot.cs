using UnityEngine;
using System.Collections;

public class RailgunShot : BulletFlexAbility {

	public RailgunShot(Transform e, GameObject bt) : base(e, bt){ }
	public RailgunShot() : base(){ }

	protected override void setValues ()
	{
		dispName = "Railgun Shot";
		desc = "Fire a powerful bullet that knocks the ship back.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityRailgunShot", typeof(Sprite));
		cost = 0;
		cooldown = 1.5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new RailgunShot (invoker, bulletPrefab);
	}

	public override bool use(){ 
		GameObject bullet = Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, invoker.rotation);
		bullet.GetComponent<Bullet>().damage *= 2;

		invoker.GetComponent<Rigidbody2D>().AddForce(invoker.transform.up * 100);

		currentCD = cooldown;
		return true;
	}
}
