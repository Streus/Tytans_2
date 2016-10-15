using UnityEngine;
using System.Collections;

public class StatusBerzerk : StatusEffect {
	
	private float armorAmount;
	private float damMultAmount;

	private float threshold;

	public StatusBerzerk(float dur, Transform t, float threshold) : base(dur, t)
	{
		name = "Berzerking";
		desc = "Becoming stronger with lower health.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectBerzerk");

		armorAmount = 0f;
		damMultAmount = 0f;

		this.threshold = threshold;
	}

	public override StatusEffect Copy ()
	{
		return new StatusBerzerk (initDuration, invoker, threshold);
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
		invokerVars.damageMultiplier -= damMultAmount;

		//calculate and apply modifiers based on current invoker health  TODO revisit these calculations
		armorAmount = 0.75f + (0.25f * ((invokerVars.health/(invokerVars.healthMax * threshold))));
		damMultAmount = 1f + (0.25f * (1 - (invokerVars.health/(invokerVars.healthMax * threshold))));

		invokerVars.armor += armorAmount;
		invokerVars.damageMultiplier += damMultAmount;
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
		invokerVars.damageMultiplier -= damMultAmount;

		//do list removal manually
		duration = 0f;
		statusList.Remove (this);
	}
}