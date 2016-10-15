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
		GameObject b = (GameObject)MonoBehaviour.Instantiate(bulPrefab, invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;

		//apply cost n such
		invoker.GetComponent<Entity>().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
