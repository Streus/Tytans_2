using UnityEngine;
using System.Collections;

public class Themis : Boss {

	public override void Awake ()
	{
		base.Awake ();

		//add abilities
		self.abilities = new Ability[6];
		self.addAbility (new FlakShot (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletThemis")), 0);
		self.addAbility (new SwordOfTruth (transform), 1);
		self.addAbility (new BalanceTheScales (transform), 2);
		self.addAbility (new Judgement (transform), 3);
		self.addAbility (new Berzerk (transform), 4);
		//justice self.addAbility(new Justice(transform), 5);
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		//-MOVEMENT-
		faceTarget ();
		self.physbody.AddForce (transform.right * self.speed);

		//-COMBAT-

		//flak shot
		useAbility(0);

		//SoT
		useAbility(1);

		//BtS and judgement
		float distToTarget = Vector2.Distance(transform.position, target.transform.position);
		useAbility(2, distToTarget < 2.5f);
		useAbility(3, distToTarget < 5f);

		//berzerk and justice
		bool phase2Threshold = self.health / self.healthMax < 0.5f;
		useAbility(4, phase2Threshold);
		//useAbility (5, phase2Threshold);
	}
}
