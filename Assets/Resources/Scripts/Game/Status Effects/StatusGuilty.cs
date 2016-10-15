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

	public override StatusEffect Copy ()
	{
		return new StatusGuilty (initDuration, invoker, armorReduction);
	}

	public override void apply ()
	{
		//do effect


		//apply debuff
		invokerVars.armor -= armorReduction;

		//remove Innocent if the subject has it
		for (int i = 0; i < statusList.Count; i++) 
		{
			if (((StatusEffect)statusList [i]).name == "Innocent") 
			{
				((StatusEffect)statusList [i]).duration = 0;
				break;
			}
		}
	}

	public override void revert ()
	{
		invokerVars.armor += armorReduction;
	}
}
