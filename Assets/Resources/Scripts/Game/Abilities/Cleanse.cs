using UnityEngine;
using System.Collections;

public class Cleanse : Ability {

	public Cleanse(Transform e) : base(e)
	{
		dispName = "Cleanse";
		desc = "Remove all status effects and gain temporary immunity to status effects";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityCleanse");
		cost = 50f;
		cooldown = 60f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Cleanse (invoker);
	}

	public override bool use ()
	{
		//remove all status effects on invoker
		Entity invokerVars = invoker.GetComponent<Entity>();
		for (int i = 0; i < invokerVars.statuses.Count; i++) {
			((StatusEffect)invokerVars.statuses [i]).duration = 0;
		}

		//apply cleansed status effect
		invokerVars.addStatus (new StatusCleansed (5f, invoker));

		invokerVars.energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
