using UnityEngine;
using System.Collections;

public class StatusCool : StatusEffect {

	private float decayAmount;

	public StatusCool(float dur, Transform t, float da) : base(dur, t)
	{
		name = "Cooling";
		desc = "Heat is dispersing.";
		icon = Resources.Load<Sprite>("Sprites/UI/Status Effects/StatusEffectENRegen");

		decayAmount = da;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusCool (initDuration, e, decayAmount);
	}

	public override void apply ()
	{
		//apply particle effect
		//TODO particle effect

		//apply regen
		invokerVars.heatDecay += decayAmount;
	}

	public override void revert ()
	{
		//remove particle effect


		//remove regen
		invokerVars.heatDecay -= decayAmount;
	}
}
