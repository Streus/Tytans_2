using UnityEngine;
using System.Collections;

// Intermidiate parent class for abilities that have a flexible bullet type
public class BulletFlexAbility : Ability {

	// flexible bullet prefab
	public GameObject bulletPrefab;

	// Constructors
	public BulletFlexAbility(Transform e, GameObject bullPre) : base(e){
		bulletPrefab = bullPre;
	}
	public BulletFlexAbility(GameObject bullPre) : base(){
		bulletPrefab = bullPre;
	}
	public BulletFlexAbility() : base(){
		bulletPrefab = null;
	}

	// Empty setValues inherited from Ability
	protected override void setValues ()
	{
		return;
	}

	// Basic Copy to be overriden by child abilities
	public override Ability Copy ()
	{
		return new BulletFlexAbility (invoker, bulletPrefab);
	}

	// Basic use to be overriden by child abilities
	public override bool use ()
	{
		return false;
	}
}
