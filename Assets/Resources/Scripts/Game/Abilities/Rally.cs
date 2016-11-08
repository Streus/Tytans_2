using UnityEngine;
using System.Collections;

public class Rally : Ability {

	public Rally(Transform e) : base(e){ }
	public Rally() : base(){ }

	protected override void setValues ()
	{
		dispName = "Rally";
		desc = "Inspire those around you to deal increased\ndamage and have increased speed.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityRally");
		cost = 20f;
		cooldown = 20f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Rally (invoker);
	}

	public override void use ()
	{
		base.use ();

		GameObject rb = Bullet.createBullet (invoker.gameObject, Resources.Load<GameObject> ("Prefabs/Bullets/RallyAura"), invoker.position, invoker.rotation);
		rb.GetComponent<RallyBullet> ().faction = Faction.NEUTRAL;
	}
}
