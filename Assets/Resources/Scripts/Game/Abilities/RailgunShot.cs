using UnityEngine;
using System.Collections;

//TODO
public class RailgunShot : Ability {

	GameObject bulletPrefab;

	public RailgunShot(Transform e, GameObject bt) : base(e){
		dispName = "Railgun Shot";
		desc = "Fire a powerful bullet that knocks the ship back.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityRailgunShot", typeof(Sprite));
		cost = 0;
		cooldown = 1.5f;
		currentCD = cooldown;
		bulletPrefab = bt;
	}

	//TODO
	public override bool use(){ 
		currentCD = cooldown;
		return false; 
	}
}
