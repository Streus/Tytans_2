using UnityEngine;
using System.Collections;

public class Player : ControlScript {

	public GameObject bullet;
	public PlayerClass myClass;
	public ArrayList learnedAbilities;

	public bool acceptInput;

	// Use this for initialization
	public override void Awake () 
	{
		base.Awake ();

		learnedAbilities = new ArrayList ();

		acceptInput = true;
	}

	public void Start(){ //DEBUG STUFF
	}

	void Update () {
		//if (!physbody.simulated)
		//	return;
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
		if (!acceptInput || !physbody.simulated || self.stunned > 0)
			return;

		//point to mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		facePoint (mousePos);

		// movement
		if (Input.GetKey (Bindings.forward)) {
			physbody.AddForce (transform.up * -self.speed);
		}
		if (Input.GetKey (Bindings.strafeL)) {
			physbody.AddForce (transform.right * self.speed);
		}
		if (Input.GetKey (Bindings.reverse)) {
			physbody.AddForce (transform.up * self.speed);
		}
		if (Input.GetKey (Bindings.strafeR)) {
			physbody.AddForce (transform.right * -self.speed);
		}

		// basic attack (abilities[0])
		if(Input.GetKey(Bindings.fire) && self.abilities[0].ready())
		{
			useAbility (0);
		}

		// class ability (abilities[1])
		if(Input.GetKey(Bindings.classAbility) && self.abilities[1].ready())
		{
			useAbility (1);
		}

		// flex ability 1 (abilities[2])
		if(Input.GetKey(Bindings.ability0) && self.abilities[2].ready())
		{
			useAbility (2);
			self.abilities[2].use();
		}

		// flex ability 2 (abilities[3])
		if(Input.GetKey(Bindings.ability1) && self.abilities[3].ready())
		{
			useAbility (3);
		}

		// flex ability 3 (abilities[4])
		if(Input.GetKey(Bindings.ability2) && self.abilities[4].ready())
		{
			useAbility (4);
		}
	}

	// Add a new ability to the self's list of learned abilities and re-sort the list
	// Return false if the ability is already learned.
	public bool learnAbility(Ability ability)
	{
		for (int i = 0; i < learnedAbilities.Count; i++)
			if (((Ability)learnedAbilities [i]).CompareTo (ability) == 0)
				return false;

		learnedAbilities.Add (ability);
		learnedAbilities.Sort (null);
		return true;
	}
}