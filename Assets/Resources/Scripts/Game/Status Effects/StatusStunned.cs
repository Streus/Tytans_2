using UnityEngine;
using System.Collections;

public class StatusStunned : StatusEffect
{
	private GameObject stunEffect;

	public StatusStunned(float dur, Transform e) : base (dur, e)
	{
		name = "Stunned";
		desc = "Unable to move or attack.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectStunned");
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusStunned (initDuration, e);
	}

	public override void apply ()
	{
		//add visual effect
		//TODO stunned viseff

		//apply debuff
		invokerVars.stunned++;
	}

	public override void revert ()
	{
		//remove visual effect


		//remove debuff
		invokerVars.stunned--;
	}
}
