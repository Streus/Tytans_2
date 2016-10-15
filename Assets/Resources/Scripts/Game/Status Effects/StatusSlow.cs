using UnityEngine;
using System.Collections;

public class StatusSlow : StatusEffect {

	private int slowAmount;

	public StatusSlow(float dur, Transform e, int sa) : base(dur, e)
	{
		name = "Slowed";
		desc = "Speed reduced.";
		icon = Resources.Load<Sprite> ("Sprites/UI/StatusEffects/StatusEffectSlow");

		slowAmount = sa;
	}

	public override StatusEffect Copy ()
	{
		return new StatusSlow (initDuration, invoker, slowAmount);
	}

	public override void apply ()
	{
		//apply effect


		//apply debuff
		slowAmount = Mathf.Min(slowAmount, invokerVars.speed);
		invokerVars.speed -= slowAmount;
	}

	public override void revert ()
	{
		//remove effect


		//remove debuff
		invokerVars.speed += slowAmount;
	}
}
