using UnityEngine;
using System.Collections;

public delegate void LearnedAbility(Ability ability);

public class Player : MonoBehaviour {

	public GameObject bullet;
	public PlayerClass myClass;
	private Rigidbody2D physbody;
	private Entity player;
	public ArrayList learnedAbilities;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		player = transform.GetComponent<Entity>();

		learnedAbilities = new ArrayList ();
	}

	void Update () {
		if (!physbody.simulated)
			return;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!physbody.simulated)
			return;

		//point to mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

		// movement
		if(Input.GetKey(Bindings.move) && !Input.GetKey(Bindings.hold))
		{
			physbody.AddForce(transform.up * -player.speed);
		}

		// basic attack (abilities[0])
		if(Input.GetKey(Bindings.ability0) && player.abilities[0].ready())
		{
			player.abilities[0].use();
		}

		// class ability (abilities[1])
		if(Input.GetKey(Bindings.classAbility) && player.abilities[1].ready())
		{
			player.abilities[1].use();
		}

		// flex ability 1 (abilities[2])
		if(Input.GetKey(Bindings.ability1) && player.abilities[2].ready())
		{
			player.abilities[2].use();
		}

		// flex ability 2 (abilities[3])
		if(Input.GetKey(Bindings.ability2) && player.abilities[3].ready())
		{
			player.abilities[3].use();
		}
	}

	// Event to use with learnAbility
	public event LearnedAbility newAbility;
	protected virtual void updatedAbilityList(Ability ability){
		if(newAbility != null)
			newAbility(ability);
	}

	// Add a new ability to the player's list of learned abilities and re-sort the list
	// Return false if the ability is already learned.
	public bool learnAbility(Ability ability)
	{
		if (learnedAbilities.Contains (ability))
			return false;

		learnedAbilities.Add (ability);
		learnedAbilities.Sort (null);
		updatedAbilityList (ability);
		return true;
	}
}