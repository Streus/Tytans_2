using UnityEngine;
using System.Collections;

public class StatusRallied : StatusEffect {

	private float damageAdditive;
	private int speedAdditive;

	public StatusRallied (float dur, Transform e, float damAdd, int spdAdd) : base (dur, e)
	{
		name = "Rallied";
		desc = "Gained increased speed and damage.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectRallied");

		damageAdditive = damAdd;
		speedAdditive = spdAdd;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusRallied (initDuration, e, damageAdditive, speedAdditive);
	}

	public override void apply ()
	{
		//play effect
		//TODO rallied viseff

		//add buff
		invokerVars.speed += speedAdditive;
		invokerVars.damageAdditive += damageAdditive;
	}

	public override void revert ()
	{
		//remove buff
		invokerVars.speed -= speedAdditive;
		invokerVars.damageAdditive -= damageAdditive;
	}
}
