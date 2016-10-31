using UnityEngine;
using System.Collections;

public class Dash : Ability {
	
	public Dash(Transform e) : base(e){ }
	public Dash() : base(){ }

	protected override void setValues ()
	{
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

	public override void use(){
		base.use ();

		Rigidbody2D body = invoker.GetComponent<Rigidbody2D>();
		body.AddForce(invoker.up * -50, ForceMode2D.Impulse);
	}
}
