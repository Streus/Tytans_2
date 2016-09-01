using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bullet;
	public PlayerClass myClass;
	public Ability specialAbility;
	private Rigidbody2D physbody;
	private Entity player;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		physbody = transform.GetComponent<Rigidbody2D>();
		player = transform.GetComponent<Entity>();

		// Basic attack TEMPORARY
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
		if(Input.GetKey(Bindings.move) && !Input.GetKey(Bindings.hold))
		{
			physbody.AddForce(transform.up * -player.speed);
		}

		// basic attack (abilities[0])
		if(Input.GetKey(Bindings.ability0) && player.abilities[0].ready())
		{
			player.abilities[0].use();
		}

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
}