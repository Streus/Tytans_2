using UnityEngine;
using System.Collections;

//TODO
public class BurstShot : Ability {

	public GameObject bulletPrefab;
	private int shotNumber;

	public BurstShot(Transform e, GameObject bt) : base(e){
		dispName = "Burst Shot";
		desc = "Fire a burst of three bullets.";
		position = 2;
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityBurstShot", typeof(Sprite));
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
		bulletPrefab = bt;
		shotNumber = 1;
	}

	//TODO
	public override bool use(){ 
		GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;

		shotNumber = shotNumber % 3;
		if (shotNumber == 0)
			currentCD = cooldown;
		else
			currentCD = 0.05f;
		shotNumber++;
		return true; 
	}
}
