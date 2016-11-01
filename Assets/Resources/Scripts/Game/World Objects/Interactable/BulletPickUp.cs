using UnityEngine;
using System.Collections;

public class BulletPickUp : MonoBehaviour {

	private SpriteRenderer image;
	private Rigidbody2D physbody;
	public GameObject bullet;
	public bool interactable;
	public float interactDelay;

	void Awake () {
		image = transform.GetComponent<SpriteRenderer> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
		bullet = null;
		interactable = false;
		interactDelay = 2f;
	}

	// Use this for initialization
	void Start () {
		if (bullet != null)
			image.sprite = bullet.GetComponent<SpriteRenderer> ().sprite;
		physbody.AddForce (Random.insideUnitCircle * 20, ForceMode2D.Impulse); 
	}
	
	// Update is called once per frame
	void Update () {
		//just for effect
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 1));

		interactDelay -= Time.deltaTime;
		if (interactDelay <= 0)
			interactable = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == GameManager.player && interactable)
		{
			GameObject drop = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/BulletPickUp"), transform.position, Quaternion.identity);
			drop.GetComponent<BulletPickUp> ().bullet = col.gameObject.GetComponent<Player> ().bullet;

			col.gameObject.GetComponent<Player> ().bullet = bullet;

			Entity playerEnt = col.transform.GetComponent<Entity> ();
			for (int i = 0; i < playerEnt.abilities.Length; i++) {
				if (playerEnt.abilities [i] is BulletFlexAbility)
					((BulletFlexAbility)playerEnt.abilities [i]).bulletPrefab = bullet;
			}
			Destroy (gameObject);
		}
	}

	public void assignBullet(string prefabName)
	{
		bullet = Resources.Load<GameObject> ("Prefabs/Bullets/" + prefabName);
		if (bullet == null)
			Debug.LogError ("Error assigning bullet prefab to pickup.  " + prefabName + " is not a valid bullet prefab.", this);
	}
}
