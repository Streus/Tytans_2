using UnityEngine;
using System.Collections;

//TODO
public class CoreOverload : Ability {
	
	public CoreOverload(Transform e) : base(e){ }
	public CoreOverload() : base(){ }

	protected override void setValues ()
	{
		dispName = "Core Overload";
		desc = "Emit a powerful shockwave that knocks back and damages enemies as well as restoring some energy.";
		image = (Sprite)Resources.Load("Sprites/UI/Abilities/AbilityCoreOverload", typeof(Sprite));
		cost = -75f;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new CoreOverload (invoker);
	}
		
	public override bool use(){
		//explosion boom boom
		//TODO

		//apply regen and reset ability
		invoker.GetComponent<Entity>().addStatus(new StatusENRegen(2f, invoker, 40f));
		currentCD = cooldown;
		return true; 
	}
}
