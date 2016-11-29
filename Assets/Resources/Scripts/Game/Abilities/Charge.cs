using UnityEngine;
using System.Collections;

public class Charge : Ability
{
	public Charge(Transform e) : base(e){ }
	public Charge() : base(){ }

	protected override void setValues ()
	{
		dispName = "Charge";
		desc = "Burst forward and stun anyone who collides with you.";
		image = (Sprite)Resources.Load<Sprite>("Sprites/UI/Abilities/AbilityCharge");
		cost = 20;
		cooldown = 5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Charge (invoker);
	}

	public override void use(){
		base.use ();

		Rigidbody2D body = invoker.GetComponent<Rigidbody2D>();
		body.AddForce(invoker.up * -25, ForceMode2D.Impulse);

		Bullet.createBullet (invoker.gameObject, Resources.Load<GameObject> ("Prefabs/Bullets/BulletCharge"), invoker.position, invoker.rotation);
	}
}
