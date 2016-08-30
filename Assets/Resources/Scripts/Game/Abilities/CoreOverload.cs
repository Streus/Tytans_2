using UnityEngine;
using System.Collections;

//TODO
public class CoreOverload : Ability {
	public CoreOverload(Transform e) : base(e){}

	public override bool use(){ return false; }
}
