using UnityEngine;
using System.Collections;

public class StatusShocked : StatusEffect {

	private float interruptInterval;
	private float interruptTimer;

	private GameObject shockEffect;

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
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusShocked(initDuration, e, interruptInterval);
	}

	public override void apply ()
	{
		shockEffect = (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> ("Prefabs/Effects/SparkEffect"), invoker, false);
	}

	public override void revert ()
	{
		MonoBehaviour.Destroy (shockEffect);
	}
}
