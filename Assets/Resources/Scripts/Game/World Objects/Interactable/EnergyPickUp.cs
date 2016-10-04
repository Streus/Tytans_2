using UnityEngine;
using System.Collections;

public class EnergyPickUp : MonoBehaviour {

	//private Rigidbody2D physbody;

	void Awake () {
		//physbody = transform.GetComponent<Rigidbody2D> ();
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
			col.gameObject.GetComponent<Entity> ().energy += 20f;
			Destroy (gameObject);
		}
	}
}
