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
			Quaternion bulletRot = Quaternion.Euler(new Vector3(0, 0, invoker.eulerAngles.z + (5f * i) - 10));
			GameObject b = (GameObject)MonoBehaviour.Instantiate(bulletPrefab, invoker.position, bulletRot);
			Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
			Bullet bullet = b.transform.GetComponent<Bullet>();
			bullet.faction = invoker.GetComponent<Entity>().faction;
			bullet.damage /= 2;
		}
			
		currentCD = cooldown;
		return false; 
	}
}
