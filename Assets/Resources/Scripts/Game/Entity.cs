﻿using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public Faction faction;
	public float health;
	public float healthMax;
	public float healthRegen;
	public float energy;
	public float energyMax;
	public float energyRegen;
	public int speed;
	public DeathType death;
	public ArrayList statuses;
	public Ability[] abilities = new Ability[8];
	public float cooldownRate;

	// Use this for initialization
	void Start () {
		statuses = new ArrayList();
		//abilities = new Ability[7]; The fuckiest of errors
	}
	
	// Update is called once per frame
	void Update () {

		// update cooldowns
		for(int i = 0; i < abilities.Length; i++)
		{
			if(abilities[i] != null){
				abilities[i].currentCD -= Time.deltaTime * cooldownRate;
				abilities[i].clampCD();
			}
			
		}
	}

	// Adds an ability to this entity's roster at index
	public bool addAbility(Ability abil, int index)
	{
		abilities[index] = abil;
		return true;
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