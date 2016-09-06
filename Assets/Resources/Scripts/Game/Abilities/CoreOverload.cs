using UnityEngine;
using System.Collections;

//TODO
public class CoreOverload : Ability {
	public CoreOverload(Transform e) : base(e){
		dispName = "Core Overload";
		desc = "Emit a powerful shockwave that knocks back and damages enemies as well as restoring some energy.";
		position = 3;
		image = (Sprite)Resources.Load("Sprites/UI/Abilities/AbilityCoreOverload", typeof(Sprite));
		cost = -75;
		cooldown = 10f;
		currentCD = cooldown;
	}

	//TODO
	public override bool use(){
		currentCD = cooldown;
		return false; 
	}
}
