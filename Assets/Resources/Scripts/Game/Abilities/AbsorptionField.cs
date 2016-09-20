using UnityEngine;
using System.Collections;

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
		
	public override bool use(){ 
		Entity invokervars = invoker.GetComponent<Entity>();
		if (invokervars.shieldMax > 0) //invoker already has a shield
			return false;

		invokervars.addStatus(new StatusAbsorptionField(5f, invoker.transform, 2f, 350f));
		invokervars.energy -= cost;
		currentCD = cooldown;
		return true; 
	}
}
