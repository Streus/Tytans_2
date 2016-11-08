﻿using UnityEngine;
using System.Collections;

public class Sacrifice : MinionAbility
{
	public Sacrifice(Transform e, ArrayList m) : base(e, m){ }
	public Sacrifice() : base(){ }

	protected override void setValues ()
	{
		dispName = "Sacrifice";
		desc = "Mark a minion as Offered\nOffered minions release health upon death.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilitySacrifice");
		cost = 10f;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Sacrifice (invoker, minions);
	}

	public override void use ()
	{
		base.use ();

		StatusOffered offered = new StatusOffered (10f, invoker, invoker.gameObject);

		for (int i = 0; i < minions.Count; i++)
		{
			Entity minion = ((GameObject)minions [i]).GetComponent<Entity> ();
			if (!(minion.hasStatus (offered)))
			{
				minion.addStatus (offered.Copy(((GameObject)minions [i]).transform));
				return;
			}
		}
		//failed to apply, refund energy cost
		invoker.GetComponent<Entity> ().energy += cost;
	}
}
