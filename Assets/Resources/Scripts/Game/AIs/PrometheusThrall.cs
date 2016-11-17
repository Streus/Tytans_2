using UnityEngine;
using System.Collections;

public class PrometheusThrall : ControlScript
{
	private Vector2 formationPosition;
	public GameObject prometheus;

	private bool atPosition;

	public override void Awake ()
	{
		base.Awake ();

		self.addAbility (new BasicShot (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletPlasma")), 0);

		atPosition = false;
	}

	public void Start()
	{
		target = GameManager.player;
	}

	// formationPosition accessor
	public Vector2 FormationPosition
	{
		get{ return formationPosition; }
		set{ 
			formationPosition = value;
			atPosition = false;
		}
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		if (!atPosition && formationPosition != Vector2.zero) {
			facePoint (formationPosition);
			physbody.AddForce (transform.up * -self.speed);
			atPosition = Vector2.Distance (transform.position, formationPosition) < 0.01f;
		} else if(target != null){
			physbody.velocity = Vector2.zero;
			faceTarget(target);
		}
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
			if(prometheus != null)
				prometheus.GetComponent<Prometheus> ().removeMinion (gameObject);
		}
	}
}
