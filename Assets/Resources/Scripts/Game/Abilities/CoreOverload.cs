using UnityEngine;
using System.Collections;

//TODO
public class CoreOverload : Ability {
	public CoreOverload(Transform e) : base(e){
		dispName = "Core Overload";
		desc = "Emit a powerful shockwave that knocks back and damages enemies as well as restoring some energy.";
		image = (Sprite)Resources.Load("Sprites/UI/Abilities/AbilityCoreOverload", typeof(Sprite));
		cost = -75;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new CoreOverload (invoker);
	}

	//TODO
	public override bool use(){
		invoker.GetComponent<Entity>().energy -= cost;
		currentCD = cooldown;
		return false; 
	}
}
