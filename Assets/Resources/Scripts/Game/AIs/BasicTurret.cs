using UnityEngine;
using System.Collections;

public class BasicTurret : ControlScript
{
	// Use this for initialization
	public void Start () 
	{
		target = GameManager.player;
		self.addAbility (new FlakShot (transform, Resources.Load<GameObject> ("Prefabs/Bullets/BulletBasic")), 0);
		self.addAbility(new BalanceTheScales(transform), 1);
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

		faceTarget (target);

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
