using UnityEngine;
using System.Collections;

public class StatusSlow : StatusEffect {

	private int slowAmount;

	GameObject slowEffect;

	public StatusSlow(float dur, Transform e, int sa) : base(dur, e)
	{
		name = "Slowed";
		desc = "Speed reduced.";
		icon = Resources.Load<Sprite> ("Sprites/UI/StatusEffects/StatusEffectSlow");

		slowAmount = sa;
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusSlow (initDuration, e, slowAmount);
	}

	public override void apply ()
	{
		//apply effect
		slowEffect = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Effects/SlowEffect"), invoker, false);

		//apply debuff
		slowAmount = Mathf.Min(slowAmount, invokerVars.speed);
		invokerVars.speed -= slowAmount;
	}

	public override void revert ()
	{
		//remove effect
		MonoBehaviour.Destroy(slowEffect);

		//remove debuff
		invokerVars.speed += slowAmount;
	}
}
