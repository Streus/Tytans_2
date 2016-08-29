﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bullet;
	private Rigidbody2D physbody;
	private Entity player;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		player = transform.GetComponent<Entity>();

		// Basic attack cooldown
		player.addAbility(new BasicShot(transform, bullet), 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//point to mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

		// movement
		if(Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift))
		{
			physbody.AddForce(transform.up * -player.speed);
		}

		// basic attack (abilities[0])
		if(Input.GetKey(KeyCode.Mouse1) && player.abilities[0].ready())
		{
			player.abilities[0].use();
		}

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
}
