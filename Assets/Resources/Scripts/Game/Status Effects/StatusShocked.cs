using UnityEngine;
using System.Collections;

public class StatusShocked : StatusEffect {

	private float interruptInterval;
	private float interruptTimer;

	public StatusShocked(float dur, Transform e, float ii) : base(dur, e)
	{
		name = "Shocked";
		desc = "Movement is being interrupted.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectShocked");

		interruptInterval = interruptTimer = ii;
	}

	public override void update (float dec)
	{
		base.update (dec);

		//interrupt movement
		interruptTimer -= dec;
		if (interruptTimer <= 0) {
			invoker.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			interruptTimer = interruptInterval;
		}

		//do effect TODO add effect

	}

	public override StatusEffect Copy ()
	{
		return new StatusShocked(initDuration, invoker, interruptInterval);
	}

	public override void apply (){ }

	public override void revert (){ }
}
