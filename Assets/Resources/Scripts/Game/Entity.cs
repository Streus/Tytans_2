using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public Faction faction;
	public int health;
	public int healthMax;
	public int energy;
	public int energyMax;
	public int speed;
	public DeathType death;
	public ArrayList statuses;
	public float[] cooldowns;
	public float[] cooldownsMax;

	// Use this for initialization
	void Start () {
		statuses = new ArrayList();
		cooldowns = new float[11];
		cooldownsMax = new float[11];
	}
	
	// Update is called once per frame
	void Update () {

		// update cooldowns
		for(int i = 0; i < cooldownsMax.Length; i++)
		{
			cooldowns[i] -= Time.deltaTime;
			if(cooldowns[i] < 0) cooldowns[i] = 0;
		}
	}

	// Add a cooldown to the list of managed cooldowns
	public void addCooldown(int index, float max)
	{
		cooldownsMax[index] = max;
		cooldowns[index] = max;
	}

	// Reset a cooldown to its max value
	public void resetCooldown(int index)
	{
		cooldowns[index] = cooldownsMax[index];
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

			break;
		default:

			break;
		}
	}
}

public enum Faction
{
	PLAYER, NEUTRAL, ENEMY
}

public enum DeathType
{
	Player
}