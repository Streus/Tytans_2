using UnityEngine;
using System.Collections;

//TODO
public class FlakShot : Ability {

	GameObject bulletPrefab;

	public FlakShot(Transform e, GameObject bt) : base(e){
		dispName = "Flak Shot";
		desc = "Fire a volley of short-range bullets.";
		position = 5;
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityFlakShot", typeof(Sprite));
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
