using UnityEngine;
using System.Collections;
using System;

// Delegate for the addStatus method
public delegate void UpdatedStatusList(StatusEffect status);

public class Entity : MonoBehaviour {

	// Faction
	public Faction faction;

	// Health vars
	public float health;
	public float healthMax;
	public float healthRegen;

	// Shield vars
	public float shieldHealth;
	public float shieldMax;
	public float shieldRegen;

	// Energy vars
	public float energy;
	public float energyMax;
	public float energyRegen;

	// Misc Stat vars
	public int speed;
	public DeathType death;
	public float cooldownRate;

	// Combat vars
	public float armor;
	public float damageAdditive;
	public int statusImmune;
	public int stunned; //TODO add stunned support

	// Misc Lists
	public ArrayList statuses;
	public Ability[] abilities = new Ability[5]; //this has to be here

	// Other Misc
	[HideInInspector] public Rigidbody2D physbody;

	// Use this for initialization
	void Start () {
		statusImmune = 0;
		armor = 0f;
		damageAdditive = 0f;
		statuses = new ArrayList();
		//abilities = new Ability[7]; The fuckiest of errors

		physbody = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!physbody.simulated)
			return;

		//update ability cooldowns
		for(int i = 0; i < abilities.Length; i++)
		{
			if(abilities[i] != null){
				abilities[i].update(Time.deltaTime * cooldownRate);
			}	
		}

		//update statuses
		for (int i = 0; i < statuses.Count; i++) {
			((StatusEffect)statuses [i]).update (Time.deltaTime);
		}

		// regen health, shield, and energy
		health += healthRegen * Time.deltaTime;
		if(health > healthMax) health = healthMax;

		shieldHealth += shieldRegen * Time.deltaTime;
		if(shieldHealth > shieldMax) shieldHealth = shieldMax;
		if(shieldHealth <= 0) shieldHealth = shieldMax = shieldRegen = 0f; //shield break state

		energy += energyRegen * Time.deltaTime;
		if(energy > energyMax) energy = energyMax;
		if(energy < 0) energy = 0;

		checkDeath();
	}

	// Adds an ability to this entity's roster at index
	public bool addAbility(Ability abil, int index)
	{
		if (index >= abilities.Length)
			return false;
		abilities[index] = abil;
		return true;
	}

	// Event code for broadcasting status list changes to listeners
	public event UpdatedStatusList changedStatuses;
	protected virtual void onChangedStatus(StatusEffect status) {
		if (changedStatuses != null)
			changedStatuses(status);
	}

	// Apply a new status to this entity
	public void addStatus(StatusEffect status)
	{
		//check immunity
		if (statusImmune > 0)
			return;

		//check for duplicate statuses
		//if there are duplicates, replace them
		for(int i = 0; i < statuses.Count; i++)
		{
			if(((StatusEffect)statuses[i]).CompareTo(status) == 0)
			{
				((StatusEffect)statuses[i]).duration = 0;
				statuses.Insert(i, status);
				status.apply();
				onChangedStatus(status);
				return;
			}
		}

		//this is a new status
		statuses.Add(status);
		status.apply();
		onChangedStatus (status);
	}

	// Verify that this Entity is still alive
	public void checkDeath()
	{
		if(health <= 0)
			die();
	}
	
	// Do any end of lifetime clean-up / effects here
	public void die()
	{
		switch(death)
		{
		case DeathType.Player:
			Instantiate (Resources.Load<GameObject> ("Prefabs/Bullets/MediumExplosion"), transform.position, Quaternion.identity);
			GameManager.manager.playerDeath ();
			break;
		case DeathType.Normal:
			Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/MediumExplosion"), transform.position, Quaternion.identity);
			break;
		}
		Destroy(gameObject);
	}
}

public enum Faction
{
	PLAYER, NEUTRAL, ENEMY
}

public enum DeathType
{
	Player, Normal
}