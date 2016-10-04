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
		cost = 0;
		cooldown = 0.5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new BasicShot (invoker, bulletPrefab);
	}

	public override bool use()
	{
		GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;
		currentCD = cooldown;
		return true;
	}
}
