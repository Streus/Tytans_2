using UnityEngine;
using System.Collections;

// Intermidiate parent class for abilities that are applied on a group of minions
public class MinionAbility : Ability {

	//arraylist of Transforms to perform actions on
	public ArrayList minions;

	// Constructors
	public MinionAbility(Transform e, ArrayList m) : base(e){
		minions = m;
	}
	public MinionAbility(ArrayList m) : base(){
		minions = m;
	}
	public MinionAbility() : base(){
		minions = null;
	}

	// Empty setValues inherited from Ability
	protected override void setValues () { }

	// Basic Copy to be overriden by child abilities
	public override Ability Copy ()
	{
		return new MinionAbility (invoker, minions);
	}

	// Basic use to be overriden by child abilities
	public override void use ()
	{
		base.use ();
	}
}
