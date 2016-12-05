using UnityEngine;
using System.Collections;
using System;

public abstract class Ability : IComparable
{	 
	// The ability's display name
	public string dispName;

	// The ability's description flavor-text
	public string desc;

	// The graphic associated with this ability
	public Sprite image;

	// The amount of heat this ability generates
	public float cost;

	// The time the invokee must wait in seconds
	public float cooldown;

	// The current cooldown
	public float currentCD;

	// The maximum number of allowed charges
	public int maxCharges; 

	// The number of charges this ability has accrued
	public int currentCharges;

	// The invoker using the ability
	public Transform invoker;

	// A basic constructor
	public Ability(Transform entity)
	{
		invoker = entity;
		maxCharges = 0;
		setValues ();
	}
	// An empty constructor
	public Ability()
	{
		invoker = null;
		maxCharges = 0;
		setValues ();
	}

	// Set the unique values of the ability
	// Necessary for empty constructor
	protected abstract void setValues();

	// Clamp the cooldown variable at zero
	public void update(float dec)
	{
		currentCD -= Mathf.Min (currentCD, dec);
		if (currentCD == 0f && currentCharges < maxCharges) 
		{
			currentCharges++;
			if(currentCharges != maxCharges)
				currentCD = cooldown;
		}
	}

	// Return the readiness state of this ability
	public bool ready()
	{
		return (currentCD <= 0f || currentCharges >= 1);
	}

	// Test the names of this ability and another ability for equivilence
	public int CompareTo(object other)
	{
		return this.dispName.CompareTo (((Ability)other).dispName);
	}

	// Create a string representation of this ability
	public override string ToString ()
	{
		return dispName + "\n" + desc + "\n" + "\nGenerates " + cost + " heat\nCooldown: " + cooldown + " seconds\nMax Charges: " + maxCharges;
	}

	// Creates a deep copy of this Ability
	// Returns the copy of this ability
	public abstract Ability Copy();

	// Invoke the ability
	public virtual void use()
	{
		invoker.GetComponent<Entity> ().heat += cost;
		if (currentCharges >= 1) {
			currentCharges--;
		}
		if(currentCharges < maxCharges || maxCharges == 0){
			currentCD = cooldown;
		}
	}
}
