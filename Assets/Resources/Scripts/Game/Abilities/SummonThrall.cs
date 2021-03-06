﻿using UnityEngine;
using System.Collections;

public class SummonThrall : MinionAbility
{
	public SummonThrall(Transform e, ArrayList m) : base(e, m){ }
	public SummonThrall() : base(){ }

	protected override void setValues ()
	{
		dispName = "Summon Thrall";
		desc = "Create a minion to follow you.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilitySummonThrall");
		cost = 10f;
		cooldown = 2.5f;
		currentCD = 0f;

		maxCharges = currentCharges = 5 * (1 + (int)GameManager.manager.difficulty);
	}

	public override Ability Copy ()
	{
		return new SummonThrall (invoker, minions);
	}

	public override void use ()
	{
		base.use ();

		GameObject minion = (GameObject)MonoBehaviour.Instantiate (Resources.Load<GameObject> ("Prefabs/Entities/PrometheusThrall"), invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision (minion.GetComponent<Collider2D> (), invoker.GetComponent<Collider2D> ());
		minion.GetComponent<Rigidbody2D> ().AddForce (minion.transform.up * -50, ForceMode2D.Impulse);
		minion.GetComponent<PrometheusThrall> ().prometheus = invoker.gameObject;
		for (int i = 0; i < minions.Count; i++)
		{
			Physics2D.IgnoreCollision (((GameObject)minions [i]).GetComponent<Collider2D> (), minion.GetComponent<Collider2D> ());
		}
		minions.Add (minion);
	}
}
