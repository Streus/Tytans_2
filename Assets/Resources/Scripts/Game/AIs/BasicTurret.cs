using UnityEngine;
using System.Collections;

public class BasicTurret : MonoBehaviour {

	private Entity self;
	private Rigidbody2D physbody;

	// Use this for initialization
	void Start () {
		physbody = transform.GetComponent<Rigidbody2D>();
		self = transform.GetComponent<Entity>();

		self.addAbility (new FlakShot (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletBasic")), 0);
		self.addAbility(new BalanceTheScales(transform), 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!physbody.simulated)
			return;

		Vector3 tarPos = GameManager.player.transform.position;
		Quaternion rot = Quaternion.LookRotation(transform.position - new Vector3(tarPos.x, tarPos.y, -20), Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

		if(self.abilities[0].ready())
		{
			self.abilities[0].use();
		}

		if(self.abilities[1].ready())
		{
			self.abilities[1].use();
		}
	}

	void OnDestroy () {
		if(self.health <= 0){
			GameObject drop = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/World/Interactable/BulletPickUp"), transform.position, Quaternion.identity);
			drop.GetComponent<BulletPickUp> ().assignBullet ("BulletPlasma");
		}
	}
}
