using UnityEngine;
using System.Collections;
using System;

public abstract class StatusEffect : IComparable{
	
	public string name;
	public string desc;
	public Sprite icon;
	public float duration;

	public bool applied;

	public Transform invoker;
	private ArrayList statusList;
	public Entity invokerVars;

	public StatusEffect(float dur, Transform t)
	{
		name = "DEFAULT";
		desc = "This status effect is not defined.";
		icon = null;
		duration = dur;

		applied = false;

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
		applied = true;
		duration -= dec;
		if (duration <= 0f) {
			revert();
			statusList.Remove (this);
		}
	}

	//change the entitiy to which this status effect will be applied
	//return false if the status has already been applied
	public bool changeSubject(Transform t)
	{
		if (applied)
			return false;
		statusList.Remove (this);
		invoker = t;
		invokerVars = invoker.GetComponent<Entity> ();
		statusList = invokerVars.statuses;
		return true;
	}

	public int CompareTo(object other)
	{
		return this.name.CompareTo (((StatusEffect)other).name);
	}

	public override string ToString()
	{
		return name + "\n" + desc;
	}
}
