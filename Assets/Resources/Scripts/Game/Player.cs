using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D physbody;
	private Entity player;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		player = transform.GetComponent<Entity>();

		// Basic attack cooldown
		player.addCooldown(5, 0.5f);
		Debug.Log(player.cooldowns.ToString());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log(player.cooldowns.ToString());
		//point to mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

		// movement
		if(Input.GetKey(KeyCode.Mouse0))
		{
			physbody.AddForce(transform.up * -player.speed);
		}

		// basic attack (cooldowns[0])
		if(Input.GetKey(KeyCode.Mouse1) && player.cooldowns[0] <= 0)
		{
			GameObject b = (GameObject)Instantiate(Resources.Load("Prefabs/Bullets/BulletBasic", typeof(GameObject)), transform.position, transform.rotation);
			Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
			Bullet bullet = b.transform.GetComponent<Bullet>();
			bullet.faction = player.faction;

			player.resetCooldown(0);
		}

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
	}
}
