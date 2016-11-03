using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

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
	public virtual void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		physbody.drag = 0;

		//add force
		physbody.AddForce(transform.up * -speed, ForceMode2D.Impulse);
	}
	
	// Check for pause, update duration
	public virtual void Update () {
		if (!physbody.simulated)
			return;

		//countdown duration left on this bullet
		duration -= Time.deltaTime;
		if(duration <= 0)
			die();
	}

	public virtual void FixedUpdate () {
		if (!physbody.simulated)
			return;
	}

	public virtual void LateUpdate () {
		if (!physbody.simulated)
			return;
	}

	public void OnTriggerEnter2D (Collider2D col) {
		
		//Entity Collision
		if(col.gameObject.tag == "Ent" && 
			faction != col.transform.GetComponent<Entity>().faction)
		{
			Color htColor = new Color(0f, 0f, 0f, 1f);

			//retrieve the entity info of the collider and creator
			Entity other = col.transform.GetComponent<Entity>();

			//check for zero/negative damage
			if (damage <= 0) {
				htColor = new Color (0f, 1f, 0f, 1f);
				other.health -= damage;
				if (other.health > other.healthMax)
					other.health = other.healthMax;
			} 
			else //damage is postive
			{
				//reduce damage by armor
				damage -= Mathf.Min (other.armor, damage - 1);

				//check for a shield
				if (other.shieldHealth > 0) {
					//do damage to the shield, and destroy it if necessary
					other.shieldHealth -= damage;
					if (other.shieldHealth < 0) {
						other.health -= -other.shieldHealth;
						other.shieldMax = other.shieldRegen = other.shieldHealth = 0;
					}

					htColor = new Color (1f, 0.7f, 0f, 1f); //hitText color for shields
				} else {
					//do damage to the entity's health pool
					other.health -= damage;

					htColor = new Color (1f, 0f, 0f, 1f); //hitText color for health
				}
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
	protected virtual void hitEffect(Collider2D col){ }

	// Preform end-of-lifetime operations like death effects, etc.
	protected virtual void die()
	{
		Destroy(gameObject);
	}

	// Create a damage indicator on the GUI layer
	// param: the world position of the bullet-entity collision
	// param: the color of the damage text
	// param: text to describe what happened in the collision, like damage done, a status application, etc
	protected void createHitText(Vector3 worldPos, Color color, string info)
	{
		GameObject hitText = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/UI/HitText"), Vector3.zero, Quaternion.identity);
		hitText.GetComponent<HitText> ().setParentPosition (worldPos);
		Text t = hitText.GetComponent<Text>();
		t.color = color;
		t.text = info;
		hitText.GetComponent<HitText> ().duration = 2f;
	}
}