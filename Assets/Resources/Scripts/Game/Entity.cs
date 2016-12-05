using UnityEngine;
using System.Collections;
using System;

// Delegate for the addStatus method
public delegate void UpdatedStatusList(StatusEffect status);

public class Entity : MonoBehaviour
{
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
	public float heat;
	public float heatMax;
	public float heatDecay;

	// Misc Stat vars
	public int speed;
	public DeathType death;
	public float cooldownRate;

	// Combat vars
	public float armor;
	public float damageAdditive;
	public int statusImmune;
	public int stunned;

	// Misc Lists
	public ArrayList statuses;
	public Ability[] abilities = new Ability[5]; //this has to be here

	// Other Misc
	[HideInInspector] public Rigidbody2D physbody;

	// Use this for initialization
	void Start ()
	{
		statusImmune = 0;
		armor = 0f;
		damageAdditive = 0f;
		statuses = new ArrayList();
		//abilities = new Ability[7]; The fuckiest of errors

		physbody = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
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

		//regen health, shield, and decay heat
		health += healthRegen * Time.deltaTime;
		if(health > healthMax) health = healthMax;

		shieldHealth += shieldRegen * Time.deltaTime;
		if(shieldHealth > shieldMax) shieldHealth = shieldMax;
		if(shieldHealth <= 0) shieldHealth = shieldMax = shieldRegen = 0f; //shield break state

		heat -= heatDecay * Time.deltaTime;
		if (heat > heatMax) //heat exceeded accepted amount
		{
			this.addStatus (new StatusFire (3f, transform, heat - heatMax));
			heat = heatMax;
		}
		if(heat < 0) heat = 0;

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
	protected virtual void onChangedStatus(StatusEffect status)
	{
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
		//if there are duplicates, replace them TODO add support for stacking effects
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

	public bool hasStatus(StatusEffect s)
	{
		for (int i = 0; i < statuses.Count; i++)
		{
			if (((StatusEffect)statuses [i]).CompareTo (s) == 0)
				return true;
		}
		return false;
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

	// Clean up internals for destruction
	public void OnDestroy()
	{
		for (int i = 0; i < statuses.Count; i++)
		{
			((StatusEffect)statuses [i]).duration = 0f;
			((StatusEffect)statuses [i]).revert ();
		}
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