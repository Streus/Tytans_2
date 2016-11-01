using UnityEngine;
using System.Collections;

public class Berzerk : Ability {
	
	public Berzerk(Transform e) : base(e){ }
	public Berzerk() : base(){ }

	protected override void setValues ()
	{
		dispName = "Berzerk";
		desc = "If below 50% health, apply Berzerking.\nBerzerking increases damage and\ndecreases armor as health decreases.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityBerzerk");
		cost = 30f;
		cooldown = 120f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Berzerk (invoker);
	}

	public override void use ()
	{
		Entity invokervars = invoker.GetComponent<Entity> ();

		if (invokervars.health / invokervars.healthMax < 0.5f) {
			invokervars.addStatus (new StatusBerzerk (1f, invoker, 0.5f));

			base.use ();
			return;
		}
	}
}
