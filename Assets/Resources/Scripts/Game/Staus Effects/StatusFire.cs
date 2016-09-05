using UnityEngine;
using System.Collections;

public class StatusFire : StatusEffect {

	private float burnAmount;

	public StatusFire(float dur, Transform t, float ba) : base(dur, t){
		name = "Burning";
		desc = "Taking damage over time.";
		icon = Resources.Load<Sprite>("Sprites/UI/StatusEffects/StatusEffectFire");

		burnAmount = ba;
	}

	public override void apply()
	{
		invokerVars.healthRegen -= burnAmount;
	}

	public override void revert()
	{
		invokerVars.healthRegen += burnAmount;
	}
}