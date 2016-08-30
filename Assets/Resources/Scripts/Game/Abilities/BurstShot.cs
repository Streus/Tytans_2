using UnityEngine;
using System.Collections;

//TODO
public class BurstShot : Ability {
	public BurstShot(Transform e) : base(e){}

	public override bool use(){ return false; }
}
