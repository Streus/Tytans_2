using UnityEngine;
using System.Collections;

//TODO
public class RailgunShot : Ability {

	GameObject bulletPrefab;

	public RailgunShot(Transform e, GameObject bt) : base(e){
		dispName = "Railgun Shot";
		desc = "Fire a powerful bullet that knocks the ship back.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityRailgunShot", typeof(Sprite));
		cost = 0;
		cooldown = 1.5f;
		currentCD = cooldown;
		bulletPrefab = bt;
	}

	//TODO
	public override bool use(){ 
		GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, invoker.rotation);
		Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
		Bullet bullet = b.transform.GetComponent<Bullet>();
		bullet.faction = invoker.GetComponent<Entity>().faction;
		bullet.damage *= 2;

		invoker.GetComponent<Rigidbody2D>().AddForce(invoker.transform.up * 100);

		currentCD = cooldown;
		return true;
	}
}
