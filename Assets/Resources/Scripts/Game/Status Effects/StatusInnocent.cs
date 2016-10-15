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

	public override StatusEffect Copy ()
	{
		return new StatusInnocent (initDuration, invoker, regenAmount);
	}

	public override void apply ()
	{
		//TODO add effect


		//add buffs
		invokerVars.healthRegen += regenAmount;
		invokerVars.energyRegen += regenAmount;

		//remove Guilty if the subject has it
		for (int i = 0; i < statusList.Count; i++) 
		{
			if (((StatusEffect)statusList [i]).name == "Guilty") 
			{
				((StatusEffect)statusList [i]).duration = 0;
				break;
			}
		}
	}

	public override void revert ()
	{
		//TODO remove effect


		//remove buffs
		invokerVars.healthRegen -= regenAmount;
		invokerVars.energyRegen -= regenAmount;
	}
}
