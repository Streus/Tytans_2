using UnityEngine;
using System.Collections;

public class StatusHPRegen : StatusEffect {

	private float regenAmount;

	public StatusHPRegen(float dur, Transform t, float ra) : base(dur, t)
	{
		name = "Healing";
		desc = "Regenerating health over time.";
		icon = null; //TODO add regen status sprite

		regenAmount = ra;
	}

	public override void apply ()
	{
		invokerVars.healthRegen += regenAmount;
	}

	public override void revert ()
	{
		invokerVars.healthRegen -= regenAmount;
	}
}
