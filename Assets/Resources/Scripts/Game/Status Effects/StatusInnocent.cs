using UnityEngine;
using System.Collections;

public class StatusInnocent : StatusEffect {

	private float regenAmount;

	public StatusInnocent(float dur, Transform e, float ra) : base(dur, e)
	{
		name = "Innocent";
		desc = "Regenerating Health and Energy";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectInnocent");

		regenAmount = ra;
	}

	public override void apply ()
	{
		//add effect


		//add buffs
		invokerVars.healthRegen += regenAmount;
		invokerVars.energyRegen += regenAmount;
	}

	public override void revert ()
	{
		//remove effect


		//remove buffs
		invokerVars.healthRegen -= regenAmount;
		invokerVars.energyRegen -= regenAmount;
	}
}
