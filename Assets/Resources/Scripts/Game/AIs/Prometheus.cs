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

		//add drops
		//TODO add drops for Prometheus
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		//TODO write Prometheus behavior
	}
}
