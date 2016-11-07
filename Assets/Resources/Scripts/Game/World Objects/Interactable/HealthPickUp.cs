using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour {

	//private Rigidbody2D physbody;
	public float healAmount;

	void Awake () {
		//physbody = transform.GetComponent<Rigidbody2D> ();
		healAmount = 20f;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		//just for effect
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 1));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == GameManager.player) 
		{
			col.gameObject.GetComponent<Entity> ().health += healAmount;
			Destroy (gameObject);
		} 
		else if (col.gameObject.tag == "Bullet") 
		{
			GameObject b = Bullet.createBullet (gameObject, Resources.Load<GameObject> ("Prefabs/Bullets/BulletHomingHealth"), transform.position, transform.rotation);
			b.GetComponent<HomingBullet> ().damage = -healAmount;
			b.GetComponent<HomingBullet> ().homingTarget = GameManager.player;
			Destroy (gameObject);
		}
	}
}
