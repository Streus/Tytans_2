using UnityEngine;
using System.Collections;

//TODO
public class Dash : Ability {
	public Dash(Transform e) : base(e){}

	public override bool use(){ return false; }
}
