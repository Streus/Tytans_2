﻿using UnityEngine;
using System.Collections;

public class BurstShot : BulletFlexAbility {
	
	private int shotNumber;

	public BurstShot(Transform e, GameObject bt) : base(e, bt){
		shotNumber = 1;
	}
	public BurstShot() : base(){ }

	protected override void setValues ()
	{
		dispName = "Burst Shot";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityBurstShot", typeof(Sprite));
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new BurstShot (invoker, bulletPrefab);
	}
		
	public override bool use(){ 
		GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;

		shotNumber = shotNumber % 3;
		if (shotNumber == 0)
			currentCD = cooldown;
		else
			currentCD = 0.05f;
		shotNumber++;
		return true; 
	}
}
