using UnityEngine;
using System.Collections;

//TODO
public class BurstShot : Ability {

	public GameObject bulletPrefab;

	public BurstShot(Transform e, GameObject bt) : base(e){
		dispName = "Burst Shot";
		desc = "Fire a burst of three bullets.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityBurstShot", typeof(Sprite));
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
		bulletPrefab = bt;
	}

	//TODO
	public override bool use(){ 
		currentCD = cooldown;
		return false; 
	}
}
