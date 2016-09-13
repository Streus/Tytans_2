﻿using UnityEngine;
using System.Collections;

//TODO
public class AbsorptionField : Ability {

	public AbsorptionField(Transform e) : base(e){
		dispName = "Absorption Field";
		desc = "Convert incoming damage into health regen for a short time.";
		image = (Sprite)Resources.Load("Sprites/UI/Abilities/AbilityAbsorptionField", typeof(Sprite));
		cost = 25;
		cooldown = 30f;
		currentCD = cooldown;
	}

	public override Ability Copy()
	{
		return new AbsorptionField (invoker);
	}

	//TODO
	public override bool use(){ 
		invoker.GetComponent<Entity>().energy -= cost;
		currentCD = cooldown;
		return false; 
	}
}
