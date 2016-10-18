using UnityEngine;
using System.Collections;

public class StatusBerzerk : StatusEffect {
	
	private float armorAmount;
	private float damAddAmount;

	private float threshold;
	private float thresholdHealth;

	public StatusBerzerk(float dur, Transform t, float threshold) : base(dur, t)
	{
		name = "Berzerking";
		desc = "Becoming stronger with lower health.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectBerzerk");

		armorAmount = 0f;
		damAddAmount = 0f;

		this.threshold = threshold;
		thresholdHealth = threshold * invokerVars.healthMax;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusBerzerk (initDuration, e, threshold);
	}

	public override void update (float dec)
	{
		//test for exit case
		if (invokerVars.health / invokerVars.healthMax >= threshold) {
			revert ();
			return;
		}

		//remove old modifiers
		invokerVars.armor -= armorAmount;
		invokerVars.damageAdditive -= damAddAmount;

		//calculate and apply modifiers based on current invoker health
		armorAmount = Mathf.Ceil(-5f * (1 - invokerVars.health/thresholdHealth));
		damAddAmount = Mathf.Ceil(10f * (1 - invokerVars.health/thresholdHealth));

		invokerVars.armor += armorAmount;
		invokerVars.damageAdditive += damAddAmount;
	}

	public override void apply ()
	{
		//add effect
		invoker.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
	}

	public override void revert ()
	{
		//remove effect
		invoker.GetComponent<SpriteRenderer>().color = Color.white;

		//remove buff
		invokerVars.armor -= armorAmount;
		invokerVars.damageAdditive -= damAddAmount;

		//do list removal manually
		duration = 0f;
		statusList.Remove (this);
	}
}