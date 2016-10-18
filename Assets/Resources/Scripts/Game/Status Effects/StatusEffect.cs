using UnityEngine;
using System.Collections;
using System;

public abstract class StatusEffect : IComparable{
	
	public string name;
	public string desc;
	public Sprite icon;
	public float duration;
	public float initDuration;

	public Transform invoker;
	protected ArrayList statusList;
	public Entity invokerVars;

	public StatusEffect(float dur, Transform t)
	{
		name = "DEFAULT";
		desc = "This status effect is not defined.";
		icon = null;
		initDuration = duration = dur;

		invoker = t;
		invokerVars = invoker.GetComponent<Entity>();
		statusList = invokerVars.statuses;
	}

	// Add an effect to the passed transform's Entity script
	public abstract void apply();

	// Remove the effect added by this instance
	public abstract void revert();

	// Decrement duration and check for termination case
	public virtual void update(float dec)
	{
		duration -= dec;
		if (duration <= 0f) {
			revert();
			statusList.Remove (this);
		}
	}

	// Create a copy of this effect with the passed invoker
	public abstract StatusEffect Copy (Transform e);

	public int CompareTo(object other)
	{
		return this.name.CompareTo (((StatusEffect)other).name);
	}

	public override string ToString()
	{
		return name + "\n" + desc;
	}
}
