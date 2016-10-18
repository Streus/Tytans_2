using UnityEngine;
using System.Collections;

public class StatusENRegen : StatusEffect {

	private float regenAmount;

	public StatusENRegen(float dur, Transform t, float ra) : base(dur, t)
	{
		name = "Recharging";
		desc = "Regenerating energy over time.";
		icon = Resources.Load<Sprite>("Sprites/UI/Status Effects/StatusEffectENRegen");

		regenAmount = ra;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusENRegen (initDuration, e, regenAmount);
	}

	public override void apply ()
	{
		//apply particle effect
		//TODO particle effect

		//apply regen
		invokerVars.energyRegen += regenAmount;
	}

	public override void revert ()
	{
		//remove particle effect


		//remove regen
		invokerVars.energyRegen -= regenAmount;
	}
}
