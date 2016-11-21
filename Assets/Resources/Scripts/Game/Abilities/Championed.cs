using UnityEngine;
using System.Collections;

public class Championed : MinionAbility
{
	public Championed() : base(){ }
	public Championed(ArrayList m) : base(m){ }
	public Championed(Transform e, ArrayList m) : base(e, m){ }

	protected override void setValues ()
	{
		dispName = "Championed";
		desc = "Transform a minion into a more powerful version of itself.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityChampioned");
		cost = 50f;
		cooldown = 5f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new Championed (invoker, minions);
	}

	public override void use ()
	{
		base.use ();

		int randomValue = (int)(Random.value * minions.Count) % minions.Count;
		GameObject minion = ((GameObject)minions [randomValue]);
		PrometheusThrall minionAI = minion.GetComponent<PrometheusThrall> ();
		Entity minionVars = minion.GetComponent<Entity> ();
		if (!minionAI.upgraded) 
		{
			minion.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Game/Entities/PrometheusChampion");
			minionVars.healthMax *= 4;
			minionVars.energyMax += 20;
			minionVars.addAbility (new Overpowered (minion.transform), 1);
			minionAI.upgraded = true;
		} 
		else 
		{
			minionVars.health = minionVars.healthMax;
			minionVars.energy = minionVars.energyMax;
		}
		//TODO add visual effect for upgrade
	}
}
