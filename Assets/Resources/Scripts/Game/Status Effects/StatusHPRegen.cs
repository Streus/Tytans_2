using UnityEngine;
using System.Collections;

public class StatusHPRegen : StatusEffect {

	private float regenAmount;

	private GameObject healEffect;

	public StatusHPRegen(float dur, Transform t, float ra) : base(dur, t)
	{
		name = "Healing";
		desc = "Regenerating health over time.";
		icon = Resources.Load<Sprite>("Sprites/UI/Status Effects/StatusEffectHPRegen");

		regenAmount = ra;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusHPRegen (initDuration, e, regenAmount);
	}

	public override void apply ()
	{
		healEffect = (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> ("Prefabs/Effects/HealEffect"), invoker, false);

		invokerVars.healthRegen += regenAmount;
	}

	public override void revert ()
	{
		MonoBehaviour.Destroy (healEffect);

		invokerVars.healthRegen -= regenAmount;
	}
}
