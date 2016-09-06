using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public BulletType type;
	public float speed;
	public float damage;
	public Faction faction;
	public bool onHit;
	public float duration;

	private Rigidbody2D physbody;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		physbody.drag = 5;

		//add force
		physbody.AddForce(transform.up * -speed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		if (!physbody.simulated)
			return;

		// countdown duration left on this bullet
		duration -= Time.deltaTime;
		if(duration <= 0)
			die();
	}

	void OnTriggerEnter2D (Collider2D col) {
		// check if the object being collided with is an Entity of another faction
		if(col.transform.gameObject.GetComponent<Entity>() != null && 
			faction != col.transform.GetComponent<Entity>().faction)
		{
			// do damage to that entity
			Entity other = col.transform.GetComponent<Entity>();
			if(onHit)
				hitEffect(col);
			other.health -= damage;
			other.checkDeath();
		}
	}

	private void hitEffect(Collider2D col){
		Entity other = col.transform.GetComponent<Entity>();
		switch(type)
		{
		case BulletType.Basic:
			
			break;
		case BulletType.Bouncing:
			
			break;
		case BulletType.Explosive:
			
			break;
		case BulletType.Flame:
			if(other != null)
				other.addStatus(new StatusFire(5f, col.transform, 5f));
			break;
		case BulletType.Splitter:
			
			break;
		}
		Destroy(transform.gameObject);
	}

	private void die()
	{
		switch(type)
		{
		case BulletType.Basic:

			break;
		case BulletType.Bouncing:

			break;
		case BulletType.Explosive:

			break;
		case BulletType.Flame:
			
			break;
		case BulletType.Splitter:

			break;
		}
		Destroy(transform.gameObject);
	}
}

public enum BulletType
{
	Basic, Bouncing, Explosive, Flame, Splitter
}