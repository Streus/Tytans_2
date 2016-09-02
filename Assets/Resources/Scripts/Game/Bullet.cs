using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public BulletType type;
	public float speed;
	public int damage;
	public Faction faction;
	public bool onHit;
	public float duration;

	private Rigidbody2D physbody;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		physbody.drag = 5;
	}
	
	// Update is called once per frame
	void Update () {
		// countdown duration left on this bullet
		duration -= Time.deltaTime;
		if(duration <= 0)
			hitEffect();

		// move
		physbody.AddForce(transform.up * -speed, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D (Collision2D col) {
		// check if the object being collided with is an Entity of another faction
		if(col.transform.gameObject.GetComponent<Entity>() != null && 
			faction != col.transform.GetComponent<Entity>().faction)
		{
			// do damage to that entity
			Entity other = col.transform.GetComponent<Entity>();
			if(onHit)
				hitEffect(other);
			other.health -= damage;
			other.checkDeath();
		}
	}

	private void hitEffect(Entity other = null){
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