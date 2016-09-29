using UnityEngine;
using System.Collections;
using System;

public abstract class Ability : IComparable{
	 
	// The ability's display name
	public string dispName;

	// The ability's description flavor-text
	public string desc;

	// The graphic associated with this ability
	public Sprite image;

	// The amount of energy this ability consumes
	public float cost;

	// The time the invokee must wait in seconds
	public float cooldown;

	// The current cooldown
	public float currentCD;

	// The invoker using the ability
	public Transform invoker;

	// A basic, empty constructor
	public Ability(Transform entity) {
		invoker = entity;
	}

	// Clamp the cooldown variable at zero
	public void clampCD() {
		if(currentCD < 0)
			currentCD = 0f;
	}

	public bool ready()
	{
		return (currentCD <= 0f) && (cost <= invoker.GetComponent<Entity>().energy);
	}

	public int CompareTo(object other)
	{
		return this.dispName.CompareTo (((Ability)other).dispName);
	}

	public override string ToString ()
	{
		return dispName + "\n" + desc + "\n" + "\nCost: " + cost + " energy\nCooldown: " + cooldown + " seconds";
	}

	// Creates a deep copy of this Ability
	// Returns the copy of this ability
	public abstract Ability Copy();

	// Invoke the ability
	// Returns true if the ability was successfully used
	// Returns false if the ability was on cooldown, or the invoker has insufficent energy
	public abstract bool use();
}
