using UnityEngine;
using System.Collections;

public class PrometheusThrall : ControlScript
{
	private Vector2 formationPosition;
	public GameObject prometheus;

	public override void Awake ()
	{
		base.Awake ();

		self.addAbility (new BasicShot (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletPlasma")), 0);
	}

	public void Start()
	{
		target = GameManager.player;
	}

	// formationPosition accessor
	public Vector2 FormationPosition
	{
		get{ return formationPosition; }
		set{ formationPosition = value; }
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		facePoint (formationPosition);
		physbody.AddForce (transform.up * -self.speed);
	}

	public void OnDestroy()
	{
		if (self.health <= 0)
		{
			if (Random.value <= 0.25f) 
			{
				string pickup = "EnergyPickup";
				if(Random.value <= 0.5)
					pickup = "HealthPickUp";
				Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/" + pickup), transform.position, transform.rotation);
			}
			prometheus.GetComponent<Prometheus> ().removeMinion (gameObject);
		}
	}
}
