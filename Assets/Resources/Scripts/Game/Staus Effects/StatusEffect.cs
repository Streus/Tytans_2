using UnityEngine;
using System.Collections;

public abstract class StatusEffect{
	
	public string name;
	public string desc;
	public Sprite icon;
	public float duration;

	public Transform invoker;
	private ArrayList statusList;
	public Entity invokerVars;

	public StatusEffect(float dur, Transform t)
	{
		name = "DEFAULT";
		desc = "This status effect is not defined.";
		icon = null;
		duration = dur;

		invoker = t;
		invokerVars = invoker.GetComponent<Entity>();
		statusList = invokerVars.statuses;
	}

	// Add an effect to the passed transform's Entity script
	public abstract void apply();

	// Remove the effect added by this instance
	public abstract void revert();

	// Decrement duration and check for termination case
	public void update(float dec)
	{
		duration -= dec;
		if (duration <= 0f) {
			revert();
			statusList.Remove (this);
		}
	}

	public override string ToString()
	{
		return "Status Effect: " + name + "\nAffecting: " + invoker.gameObject.name + "\nDuration: " + duration.ToString ("###");
	}
}
