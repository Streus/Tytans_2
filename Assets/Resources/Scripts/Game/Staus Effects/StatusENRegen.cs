using UnityEngine;
using System.Collections;

public class StatusENRegen : StatusEffect {

	private float regenAmount;

	public StatusENRegen(float dur, Transform t, float ra) : base(dur, t)
	{
		name = "Recharging";
		desc = "Regenerating energy over time.";
		icon = null; //TODO add sprite fo this mofo

		regenAmount = ra;
	}

	public override void apply ()
	{
		invokerVars.energyRegen += regenAmount;
	}

	public override void revert ()
	{
		invokerVars.energyRegen -= regenAmount;
	}
}
