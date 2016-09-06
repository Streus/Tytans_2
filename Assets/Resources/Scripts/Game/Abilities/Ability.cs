using UnityEngine;
using System.Collections;
using System;

public abstract class Ability : IComparable{
	 
	// The ability's display name
	public string dispName;

	// The ability's description flavor-text
	public string desc;

	// The ability's position in the array of all abilities
	public int position;

	// The graphic associated with this ability
	public Sprite image;

	// The amount of energy this ability consumes
	public int cost;

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

	// Decrement the cooldown variable
	//Param: A multipier to reduce cooldown time
	public void clampCD() {
		if(currentCD < 0)
			currentCD = 0f;
	}

	public bool ready()
	{
		return currentCD <= 0f;
	}

	public int CompareTo(object other)
	{
		return this.dispName.CompareTo (((Ability)other).dispName);
	}

	// Invoke the ability
	// Returns true if the ability was successfully used
	// Returns false if the ability was on cooldown, or the invoker has insufficent energy
	public abstract bool use();
}
