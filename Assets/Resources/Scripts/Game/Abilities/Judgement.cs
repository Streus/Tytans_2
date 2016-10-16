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
		GameObject bulletPrefab = Resources.Load<GameObject> ("Prefabs/Bullets/BulletJudgement");
		Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, invoker.rotation);

		invoker.GetComponent<Entity> ().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
