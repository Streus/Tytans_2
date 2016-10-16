using UnityEngine;
using System.Collections;

public class DaedalusMissle : Ability {

	GameObject bulletPrefab;

	public DaedalusMissle(Transform e) : base(e){
		bulletPrefab = Resources.Load<GameObject> ("Prefabs/Bullets/BulletDaedalus");
	}
	public DaedalusMissle() : base(){ }

	protected override void setValues ()
	{
		dispName = "Daedalus Missle";
		desc = "Fire a powerful explosive missle.";
		image = Resources.Load<Sprite>("Sprites/UI/Abilities/AbilityDaedalusMissle");
		cost = 25;
		cooldown = 5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new DaedalusMissle (invoker);
	}

	public override bool use()
	{
		Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, invoker.rotation);
		invoker.GetComponent<Entity> ().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
