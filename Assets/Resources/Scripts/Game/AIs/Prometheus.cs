using UnityEngine;
using System.Collections;

public class Prometheus : Boss {

	//list of minions this AI can use its MinionAbilities on
	private ArrayList minions;

	public override void Awake()
	{
		base.Awake ();

		minions = new ArrayList (20);

		//add abilities
		self.abilities = new Ability[6];
		//TODO add abilites to Prometheus
		self.addAbility(new SummonThrall(transform, minions), 0);
		self.addAbility(new Rally(transform), 1);
		self.addAbility (new Sacrifice (transform, minions), 2);
		self.addAbility (new GiftOfFire (transform, minions), 3);

		//add drops
		//TODO add drops for Prometheus
		bulletDrops = new string[]{  };
		Transform temp = GameManager.player.transform;
		abilityDrops = new Ability[]{  };
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		//TODO write Prometheus behavior
		useAbility (0, minions.Count < 30);
		useAbility (1);
		useAbility (2);
		useAbility (3);

		for (int i = 0; i < minions.Count; i++) //DEBUG CODE
		{
			if(Random.value < 0.01)
				((GameObject)minions [i]).GetComponent<PrometheusThrall> ().FormationPosition = (Vector2)transform.position + Random.insideUnitCircle * 10;
		}
	}

	public void removeMinion(GameObject e)
	{
		minions.Remove (e);
	}
}
