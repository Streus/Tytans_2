using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

	public BulletType type;
	public float speed;
	public float damage;
	public Faction faction;
	public bool onHit;
	public float duration;

	private Rigidbody2D physbody;

	// The GameObject that made this bullet
	public GameObject creator;

	// Create a bullet gameobject
	// NOTE: when instPos and instRot are left to their default values, they recieve 
	// creator.transform.position and creator.transform.rotation respectively.
	public static GameObject createBullet(GameObject creator, GameObject bullet, Vector3 instPos, Quaternion instRot)
	{
		GameObject b = (GameObject)Instantiate(bullet, instPos, instRot);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), creator.GetComponent<Collider2D>());
		Bullet bulScr = b.transform.GetComponent<Bullet>();
		bulScr.faction = creator.GetComponent<Entity>().faction;
		bulScr.creator = creator;

		//add on damage
		Entity origin = creator.transform.GetComponent<Entity> ();
		bulScr.damage += origin.damageAdditive;

		return b;
	}

	// Initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		physbody.drag = 0;

		//add force
		physbody.AddForce(transform.up * -speed, ForceMode2D.Impulse);

		if(type == BulletType.Explosion)
			GameManager.cameraController.shakeCamera (0.1f, 0.2f);
	}
	
	// Check for pause, update duration
	void Update () {
		if (!physbody.simulated)
			return;

		//countdown duration left on this bullet
		duration -= Time.deltaTime;
		if(duration <= 0)
			die();
	}

	void OnTriggerEnter2D (Collider2D col) {
		
		//Entity Collision
		if(col.gameObject.tag == "Ent" && 
			faction != col.transform.GetComponent<Entity>().faction)
		{
			Color htColor = new Color(0f, 0f, 0f, 1f);

			//retrieve the entity info of the collider and creator
			Entity other = col.transform.GetComponent<Entity>();

			//reduce damage by armor
			damage -= Mathf.Min (other.armor, damage - 1);

			//check for a shield
			if(other.shieldHealth > 0)
			{
				//do damage to the shield, and destroy it if necessary
				other.shieldHealth -= damage;
				if(other.shieldHealth < 0)
				{
					other.health -= -other.shieldHealth;
					other.shieldMax = other.shieldRegen = other.shieldHealth = 0;
				}

				htColor = new Color(1f, 0.7f, 0f, 1f); //hitText color for shields
			}
			else
			{
				//do damage to the entity's health pool
				other.health -= damage;

				htColor = new Color(1f, 0f, 0f, 1f); //hitText color for health
			}

			//create a hitText
			createHitText(col.transform.position, htColor, damage.ToString());

			//do fancy stuff
			hitEffect(col);

			//check for bullet death
			if(onHit)
				die();

			//tell the entity to check for death state
			other.checkDeath();
		}

		//Indes Collision
		if (col.gameObject.tag == "Indes") 
		{
			if (onHit)
				die ();
		}
	}

	// Preform on-hit effects
	private void hitEffect(Collider2D col){
		Entity other = col.transform.GetComponent<Entity>();
		switch(type)
		{
		case BulletType.Basic:
			
			break;
		case BulletType.Bouncing:
			
			break;
		case BulletType.Explosion:
			break;
		case BulletType.Explosive:
			
			break;
		case BulletType.Flame:
			if(other != null && Random.value < 0.33f)
				other.addStatus(new StatusFire(10f, col.transform, 1f));
			break;
		case BulletType.Judgement:
			
			break;
		case BulletType.Spark:
			if (other != null)
				other.addStatus (new StatusShocked (5f, col.transform, 0.5f));
			break;
		case BulletType.Splitter:
			
			break;
		case BulletType.Verdict:
			if(other != null){
				float r = Random.value;
				if (r <= 0.5)
					other.addStatus (new StatusGuilty (7f, col.transform, 5f));
				else
					other.addStatus (new StatusInnocent (7f, col.transform, 2f));
			}
			break;
		case BulletType.StatusSharing:
			if (other != null) {
				ArrayList shared = creator.GetComponent<Entity> ().statuses;
				foreach (object status in shared) {
					other.addStatus (((StatusEffect)status).Copy (other.transform));
				}
			}
			break;
		}
	}

	// Preform end-of-lifetime operations like death effects, etc.
	private void die()
	{
		switch(type)
		{
		case BulletType.Basic:

			break;
		case BulletType.Bouncing:

			break;
		case BulletType.Explosion: 
			break;
		case BulletType.Explosive:
			Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/MediumExplosion"), transform.position, Quaternion.identity);
			break;
		case BulletType.Flame:
			
			break;
		case BulletType.Judgement:
			Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/JudgementTrap"), transform.position, Quaternion.identity);
			break;
		case BulletType.Spark:

			break;
		case BulletType.Splitter:

			break;
		case BulletType.Verdict:

			break;
		}
		Destroy(gameObject);
	}

	// Create a damage indicator on the GUI layer
	// param: the world position of the bullet-entity collision
	// param: the color of the damage text
	// opt param: text to describe what happened in the collision, like damage done, a status application, etc
	private void createHitText(Vector3 worldPos, Color color, string info)
	{
		GameObject hitText = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/UI/HitText"), Vector3.zero, Quaternion.identity);
		hitText.GetComponent<HitText> ().setParentPosition (worldPos);
		Text t = hitText.GetComponent<Text>();
		t.color = color;
		t.text = info;
		hitText.GetComponent<HitText> ().duration = 2f;
	}
}

public enum BulletType
{
	Basic, Bouncing, Explosion, Explosive, Flame, Judgement, Spark, Splitter, StatusSharing, Verdict
}