using UnityEngine;
using System.Collections;

public class StatusGuilty : StatusEffect {

	private float armorReduction;

	public StatusGuilty(float dur, Transform e, float ar) : base(dur, e)
	{
		name = "Guilty";
		desc = "Taking additional damage";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectGuilty");

		armorReduction = ar;
	}

	public override void apply ()
	{
		//do effect


		//apply debuff
		invokerVars.armor -= armorReduction;
	}

	public override void revert ()
	{
		invokerVars.armor += armorReduction;
	}
}
