using UnityEngine;
using System.Collections;

public class SwordOfTruth : Ability {

	public SwordOfTruth(Transform e) : base(e){ }
	public SwordOfTruth() : base() { }

	protected override void setValues ()
	{
		dispName = "Sword of Truth";
		desc = "Fire a double sided laser.\nOne side applies Innocent, the other Guilty";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilitySwordOfTruth");
		cost = 40f;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new SwordOfTruth (invoker);
	}

	public override bool use ()
	{
		//create base sword part
		GameObject bulPrefab = Resources.Load<GameObject>("Prefabs/Bullets/BulletSoTHilt");
		Bullet.createBullet (invoker.gameObject, bulPrefab, invoker.position, invoker.rotation);

		//apply cost n such
		invoker.GetComponent<Entity>().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
