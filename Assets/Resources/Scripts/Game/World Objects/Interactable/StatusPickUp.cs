using UnityEngine;
using System.Collections;

public class StatusPickUp : MonoBehaviour {

	private SpriteRenderer image;
	private Rigidbody2D physbody;
	public StatusEffect status;

	void Awake () {
		image = transform.GetComponent<SpriteRenderer> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
		status = null;
	}

	// Use this for initialization
	void Start () {
		if (status != null){
			assignSprite (status);
		}
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
			status.changeSubject(col.transform);
			col.gameObject.GetComponent<Entity>().addStatus(status);
			Destroy (gameObject);
		}
	}

	public void assignSprite(StatusEffect status)
	{
		switch (status.name) {
		case "Burning":
			//TODO set unique sprite
			break;
		case "Healing":
			//TODO set unique sprite
			break;
		case "Recharging":
			//TODO set unique sprite
			break;
		default:
			Debug.LogError (status.name + " does not have a unique pickup sprite yet!");
			break;
		}
	}

	public void assignStatus(StatusEffect status)
	{
		this.status = status;
		assignSprite (status);
	}
}
