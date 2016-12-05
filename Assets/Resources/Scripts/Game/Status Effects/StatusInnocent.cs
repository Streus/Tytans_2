using UnityEngine;
using System.Collections;

public class StatusInnocent : StatusEffect {

	private float regenAmount;

	GameObject effect;

	public StatusInnocent(float dur, Transform e, float ra) : base(dur, e)
	{
		name = "Innocent";
		desc = "Regenerating Health and Energy";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectInnocent");

		regenAmount = ra;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusInnocent (initDuration, e, regenAmount);
	}

	public override void apply ()
	{
		//add effect
		effect = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Effects/InnocentEffect"), invoker, false);

		//add buffs
		invokerVars.healthRegen += regenAmount;
		invokerVars.heatDecay += regenAmount;

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
		//remove effect
		MonoBehaviour.Destroy(effect);

		//remove buffs
		invokerVars.healthRegen -= regenAmount;
		invokerVars.heatDecay -= regenAmount;
	}
}
