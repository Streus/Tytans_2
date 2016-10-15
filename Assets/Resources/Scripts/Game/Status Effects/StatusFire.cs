using UnityEngine;
using System.Collections;

public class StatusFire : StatusEffect {

	private float burnAmount;

	private GameObject fireEffect;

	public StatusFire(float dur, Transform t, float ba) : base(dur, t)
	{
		name = "Burning";
		desc = "Taking damage over time.";
		icon = Resources.Load<Sprite>("Sprites/UI/Status Effects/StatusEffectFire");

		burnAmount = ba;
	}

	public override StatusEffect Copy ()
	{
		return new StatusFire (initDuration, invoker, burnAmount);
	}

	public override void apply()
	{
		fireEffect = (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> ("Prefabs/Effects/FireEffect"), invoker, false);

		invokerVars.healthRegen -= burnAmount;
	}

	public override void revert()
	{
		MonoBehaviour.Destroy (fireEffect);
		invokerVars.healthRegen += burnAmount;
	}
}