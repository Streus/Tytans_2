using UnityEngine;
using System.Collections;

public class Prometheus : Boss {

	//list of minions this AI can use its MinionAbilities on
	private ArrayList minions;

	//formation stuff
	private MinionFormation[] formList;
	private int currentFormation;
	private float rotationDelay;
	private float currentDelay;
	private bool sweep;

	public Vector2 leftSideMarker;
	public Vector2 rightSideMarker;

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
			new PolyFormation(false, PolyFormation.LINE),
			new PolyFormation(true, PolyFormation.HEXAGON) 
		};
		for (int i = 0; i < formList.Length; i++) {
			formList [i].rescale (3f);
		}
		formList [0].rescale (12f);
		currentFormation = 0;
		rotationDelay = 10f;
		currentDelay = 0.5f;
		sweep = false;

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
		if(currentDelay <= 0)
		{
			currentDelay = rotationDelay;
			if (!sweep) {
				formList [0].recenter (leftSideMarker);
				distributeMinions (formList [0]);
				sweep = true;
			} else {
				shiftPositions (rightSideMarker - leftSideMarker);
				sweep = false;
			}
		}

		if (Vector2.Distance (transform.position, GameManager.player.transform.position) < 3f && !sweep)
		{
			formList [1].recenter (transform.position);
			distributeMinions (formList [1]);
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
