using UnityEngine;
using System.Collections;

public class GiftOfFire : MinionAbility
{
	public GiftOfFire(Transform e, ArrayList m) : base(e, m){ }
	public GiftOfFire() : base(){ }

	protected override void setValues ()
	{
		dispName = "Gift of Fire";
		desc = "Bless a minion with orbiting bullets.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilitySacrifice");
		cost = 30f;
		cooldown = 15f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Sacrifice (invoker, minions);
	}

	public override void use ()
	{
		base.use ();

		for (int i = 0; i < minions.Count; i++)
		{
			Entity minion = ((GameObject)minions [i]).GetComponent<Entity> ();
			minion.addStatus (new StatusGiftOfFire (10f, minion.transform));
		}
	}
}
