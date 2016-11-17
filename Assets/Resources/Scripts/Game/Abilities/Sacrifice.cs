using UnityEngine;
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

		int randomIndex = (int)(Random.value * minions.Count);
		Entity minion = ((GameObject)minions [randomIndex]).GetComponent<Entity> ();
		minion.addStatus (new StatusOffered (20f, invoker, invoker.gameObject));
	}
}
