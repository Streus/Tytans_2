using UnityEngine;
using System.Collections;

public class Prometheus : Boss {

	//list of minions this AI can use its MinionAbilities on
	private ArrayList minions;

	//formation stuff
	private MinionFormation[] formList;
	private float rotationDelay;
	private float currentDelay;
	private bool sweep;
	public bool abilityPermission;

	public Vector2 leftSideMarker;
	public Vector2 rightSideMarker;
	private Vector2 targetPoint;

	public override void Awake()
	{
		base.Awake ();

		minions = new ArrayList (20);

		//add abilities
		self.abilities = new Ability[6];
		self.addAbility(new SummonThrall(transform, minions), 0);
		self.addAbility(new Rally(transform), 1);
		self.addAbility (new Sacrifice (transform, minions), 2);
		self.addAbility (new GiftOfFire (transform, minions), 3);
		self.addAbility (new Championed (transform, minions), 4);

		//formation related stuff
		formList = new MinionFormation[]{ 
			new PolyFormation(false, PolyFormation.LINE),
			new PolyFormation(true, PolyFormation.HEXAGON) 
		};
		formList [0].rescale (12f);
		formList [1].rescale (1.5f);
		rotationDelay = 10f;
		currentDelay = 0.5f;
		sweep = false;

		//add drops
		//TODO add drops for Prometheus
		bulletDrops = new string[]{ "BulletSpark", "BulletPlasma" };
		Transform temp = GameManager.player.transform;
		abilityDrops = new Ability[]{ new Overpowered(temp) };
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		//TODO write Prometheus movement
		useAbility (0, minions.Count < 30);
		useAbility (2);
		useAbility (4, self.health/self.healthMax < 0.5f);

		if (Vector2.Distance (transform.position, targetPoint) > 0.1f) {
			facePoint (targetPoint);
			physbody.AddForce (transform.up * -self.speed);
		} else if(target != null)
			faceTarget (target);

		//minion formation updating
		currentDelay -= Time.deltaTime;
		if(currentDelay <= 0)
		{
			currentDelay = rotationDelay;
			if (!sweep) {
				formList [0].recenter (leftSideMarker);
				targetPoint = rightSideMarker;
				distributeMinions (formList [0]);
				abilityPermission = false;
			} else {
				shiftPositions (rightSideMarker - leftSideMarker);
				targetPoint = leftSideMarker;
				abilityPermission = true;
				useAbility (3);
			}
			sweep = !sweep;
		}
		if (GameManager.player != null && Vector2.Distance (transform.position, GameManager.player.transform.position) < 3f && !sweep)
		{
			formList [1].recenter (transform.position);
			distributeMinions (formList [1]);
			useAbility (1);
		}
	}

	public override void OnDestroy ()
	{
		base.OnDestroy ();

		for (int i = 0; i < minions.Count; i++)
		{
			((GameObject)minions [i]).GetComponent<Entity> ().die ();
		}
	}

	public void removeMinion(GameObject e)
	{
		minions.Remove (e);
	}

	private void distributeMinions(MinionFormation mf)
	{
		Vector2[] positions = mf.distribute (minions.Count);
		for (int i = 0; i < minions.Count; i++){
			((GameObject)minions [i]).GetComponent<PrometheusThrall> ().FormationPosition = positions [i];
		}
	}

	// Shift all minion positions by an offset
	private void shiftPositions(Vector2 offset)
	{
		for (int i = 0; i < minions.Count; i++)
		{
			PrometheusThrall minion = ((GameObject)minions [i]).GetComponent<PrometheusThrall> ();
			minion.FormationPosition = minion.FormationPosition + offset;
		}
	}
}
