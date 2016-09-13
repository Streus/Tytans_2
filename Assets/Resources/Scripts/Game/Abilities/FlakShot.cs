using UnityEngine;
using System.Collections;

//TODO
public class FlakShot : Ability {

	GameObject bulletPrefab;

	public FlakShot(Transform e, GameObject bt) : base(e){
		dispName = "Flak Shot";
		desc = "Fire a volley of short-range bullets.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityFlakShot", typeof(Sprite));
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
		bulletPrefab = bt;
	}

	public override Ability Copy ()
	{
		return new FlakShot (invoker, bulletPrefab);
	}
		
	public override bool use(){
		for(int i = 0; i < 5; i++){
			Quaternion randomizedRot = Quaternion.Euler(new Vector3(0, 0, invoker.eulerAngles.z + (float)Random.Range(-20, 20)));
			GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, randomizedRot);
			Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
			Bullet bullet = b.transform.GetComponent<Bullet>();
			bullet.faction = invoker.GetComponent<Entity>().faction;
			bullet.damage /= 2;
		}
			
		currentCD = cooldown;
		return false; 
	}
}
