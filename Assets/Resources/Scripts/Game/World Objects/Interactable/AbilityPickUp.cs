using UnityEngine;
using System.Collections;

public class AbilityPickUp : MonoBehaviour {

	private SpriteRenderer image;
	private Rigidbody2D physbody;
	public Ability ability;

	void Awake () {
		image = transform.GetComponent<SpriteRenderer> ();
		physbody = transform.GetComponent<Rigidbody2D> ();
		ability = null;
	}

	void Start () {
		if (ability != null)
			image.sprite = ability.image;
		physbody.AddForce (Random.insideUnitCircle * 20, ForceMode2D.Impulse); 
	}

	void Update () {
		//just for effect
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 1));
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == GameManager.player) 
		{
			if(GameManager.player.GetComponent<Player> ().learnAbility (ability.Copy ()))
				Destroy (gameObject);
		}
	}

	public void assignAbility(Ability a)
	{
		ability = a;
		image.sprite = ability.image;
	}
}