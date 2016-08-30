using UnityEngine;
using System.Collections;

//TODO
public class AbsorptionField : Ability {
	public AbsorptionField(Transform e) : base(e){}

	public override bool use(){ return false; }
}
