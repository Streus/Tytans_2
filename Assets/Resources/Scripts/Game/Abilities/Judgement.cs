using UnityEngine;
using System.Collections;

public class Judgement : Ability {

	public Judgement(Transform e) : base(e){ }
	public Judgement() : base(){ }

	protected override void setValues ()
	{
		dispName = "Judegement";
		desc = "Create a vortex that traps everything that enters.\nThe vortex lasts 5 seconds.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityJudgement");
		cost = 15f;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Judgement (invoker);
	}

	public override bool use ()
	{
		GameObject b = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/BulletJudgement"), invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;

		invoker.GetComponent<Entity> ().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
