﻿using UnityEngine;
using System.Collections;

public class Prometheus : Boss {

	//list of minions this AI can use its MinionAbilities on
	private ArrayList minions;

	//formation stuff
	private MinionFormation[] formList;
	private int currentFormation;
	private float rotationDelay;
	private float currentDelay;

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

		//formation related stuff
		formList = new MinionFormation[]{ 
			new PolyFormation(true, PolyFormation.SQUARE), 
			new PolyFormation(false, PolyFormation.TRAPEZOID), 
			new PolyFormation(true, PolyFormation.HEXAGON) 
		};
		currentFormation = 0;
		rotationDelay = 10f;
		currentDelay = rotationDelay;

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

		//minion formation updating
		currentDelay -= Time.deltaTime;
		if(currentDelay <= 0){
			currentDelay = rotationDelay;
			formList [currentFormation].recenter (transform.position);
			Vector2[] positions = formList [currentFormation].distribute (minions.Count);
			for (int i = 0; i < minions.Count; i++)
			{
				((GameObject)minions [i]).GetComponent<PrometheusThrall> ().FormationPosition = positions [i];
			}
			//DEBUG
			Debug.Log(formList[currentFormation].ToString());
			string str = "";
			for (int i = 0; i < positions.Length; i++)
			{
				str += positions [i].ToString () + "\n";
			}
			Debug.Log (str);

			currentFormation = (currentFormation + 1) % formList.Length;
		}
	}

	public void removeMinion(GameObject e)
	{
		minions.Remove (e);
	}
}
