using UnityEngine;
using System.Collections;

public class BulletPickUp : MonoBehaviour {

	private SpriteRenderer image;
	private Rigidbody2D physbody;
	public GameObject bullet;

	void Awake () {
		image = transform.GetComponent<SpriteRenderer> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
		bullet = null;
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
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == GameManager.player) 
		{
			//TODO this doesn't update the gameobject being used by instantiated abilities
			col.gameObject.GetComponent<Player> ().bullet = bullet;
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
