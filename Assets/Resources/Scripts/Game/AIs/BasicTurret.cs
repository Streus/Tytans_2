using UnityEngine;
using System.Collections;

public class BasicTurret : MonoBehaviour {

	private Entity self;
	private Rigidbody2D physbody;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		self = transform.GetComponent<Entity>();


		self.addAbility(new BasicShot(transform, Resources.Load<GameObject>("Prefabs/Bullets/BulletPlasma")), 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!physbody.simulated)
			return;

		Vector3 tarPos = GameManager.player.transform.position;
		Quaternion rot = Quaternion.LookRotation(transform.position - tarPos);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

		//Debug.Log(self.abilities[0].currentCD);
		//if(self.abilities[0].ready())
		//{
			self.abilities[0].use();
		//}
	}

	void OnDestroy () {
		if(self.health <= 0){
			GameObject drop = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/BulletPickUp"), transform.position, Quaternion.identity);
			drop.GetComponent<BulletPickUp> ().assignBullet ("BulletPlasma");
		}
	}
}
