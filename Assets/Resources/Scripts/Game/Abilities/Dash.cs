using UnityEngine;
using System.Collections;

//TODO
public class Dash : Ability {
	public Dash(Transform e) : base(e){
		dispName = "Dash";
		desc = "Gain a burst of forward speed";
		image = (Sprite)Resources.Load("Sprites/UI/Abilities/AbilityDash", typeof(Sprite));
		cost = 10;
		cooldown = 3f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Dash (invoker);
	}

	public override bool use(){
		Rigidbody2D body = invoker.GetComponent<Rigidbody2D>();
		body.AddForce(invoker.up * -70, ForceMode2D.Impulse);
		invoker.GetComponent<Entity>().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
