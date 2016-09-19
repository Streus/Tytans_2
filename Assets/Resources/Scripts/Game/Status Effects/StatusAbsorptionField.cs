using UnityEngine;
using System.Collections;

public class StatusAbsorptionField : StatusEffect {

	private float regenAmount;
	private float shieldAmount;
	GameObject shieldEffect;

	public StatusAbsorptionField(float dur, Transform t, float ra, float sa) : base(dur, t)
	{
		name = "Damage Absorption";
		desc = "Absorbing damage to convert into health regeneration";
		icon = Resources.Load<Sprite>("Sprites/UI/Status Effects/StatusEffectAbsorptionField");

		regenAmount = ra;
		shieldAmount = sa;
	}

	public override void apply ()
	{
		//create visual effect
		shieldEffect = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Effects/AbsorbShieldEffect"), invoker, false);

		//apply shield to invoker
		invokerVars.shieldMax = invokerVars.shieldHealth = shieldAmount;
		invokerVars.shieldRegen = shieldAmount;
	}

	public override void revert ()
	{
		//remove visual
		MonoBehaviour.Destroy(shieldEffect);

		//remove shield
		invokerVars.shieldMax = invokerVars.shieldHealth = invokerVars.shieldRegen = 0;
	}
}
