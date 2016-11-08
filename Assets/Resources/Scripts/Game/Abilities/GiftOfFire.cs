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
		cooldown = 7f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Sacrifice (invoker, minions);
	}

	public override void use ()
	{
		base.use ();

		StatusGiftOfFire gof = new StatusGiftOfFire (10f, invoker);

		for (int i = 0; i < minions.Count; i++)
		{
			Entity minion = ((GameObject)minions [i]).GetComponent<Entity> ();
			if (!(minion.hasStatus (gof)))
			{
				minion.addStatus (gof.Copy(((GameObject)minions [i]).transform));
				return;
			}
		}
		//failed to apply, refund energy cost
		invoker.GetComponent<Entity> ().energy += cost;
	}
}
