using UnityEngine;
using System.Collections;

public class StatusCleansed : StatusEffect {

	private GameObject cleansedEffect;

	public StatusCleansed(float dur, Transform t) : base(dur, t)
	{
		name = "Cleansed";
		desc = "Immune to all statuses\nfor a short time.";
		icon = Resources.Load<Sprite> ("Sprites/UI/Status Effects/StatusEffectCleansed");
	}

	public override StatusEffect Copy ()
	{
		return new StatusCleansed (initDuration, invoker);
	}

	public override void apply ()
	{
		//apply particle effect
		cleansedEffect = (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> ("Prefabs/Effects/CleansedEffect"), invoker, false);

		//apply immunity
		invokerVars.statusImmune++;
	}

	public override void revert ()
	{
		//remove particle effect
		MonoBehaviour.Destroy(cleansedEffect);

		//remove immunity
		invokerVars.statusImmune--;
	}
}
