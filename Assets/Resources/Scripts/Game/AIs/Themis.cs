using UnityEngine;
using System.Collections;

public class Themis : Boss {

	private int strafeDirection;

	public override void Awake ()
	{
		base.Awake ();

		strafeDirection = 1;

		//add abilities
		self.addAbility (new Justice (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletThemis")), 0);
		self.addAbility (new SwordOfTruth (transform), 1);
		self.addAbility (new BalanceTheScales (transform), 2);
		self.addAbility (new Judgement (transform), 3);
		self.addAbility (new Berzerk (transform), 4);

		self.abilities [4].currentCD = 0;
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		//-MOVEMENT-
		faceTarget ();
		RaycastHit2D rightProbe = Physics2D.Raycast (transform.position, transform.right, 1.5f, LayerMask.GetMask("World"));
		RaycastHit2D leftProbe = Physics2D.Raycast (transform.position, -transform.right, 1.5f, LayerMask.GetMask("World"));
		Debug.Log ((rightProbe.collider != null).ToString ());
		if (rightProbe.collider != null)
			strafeDirection = 1;
		else if (leftProbe.collider != null) 
			strafeDirection = -1;
		self.physbody.AddForce (transform.right * self.speed * strafeDirection);

		//-COMBAT-

		//justice
		useAbility(0);

		//SoT
		useAbility(1);

		//BtS and judgement
		float distToTarget = Vector2.Distance(transform.position, target.transform.position);
		useAbility(2, distToTarget < 2.5f);
		useAbility(3, distToTarget < 5f);

		//berzerk
		bool phase2Threshold = self.health / self.healthMax < 0.5f;
		useAbility(4, phase2Threshold);
	}
}
