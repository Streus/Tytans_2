﻿using UnityEngine;
using System.Collections;

public class BasicShot : Ability {

	GameObject bulletPrefab;

	public BasicShot(Transform entity, GameObject bt) : base(entity){
		dispName = "Shoot";
		desc = "Fire a bullet.";
		image = null;
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
		bulletPrefab = bt;
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