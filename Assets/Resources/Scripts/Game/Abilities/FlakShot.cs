using UnityEngine;
using System.Collections;

public class FlakShot : BulletFlexAbility {

	public FlakShot(Transform e, GameObject bt) : base(e, bt){ }
	public FlakShot() : base(){ }

	protected override void setValues ()
	{
		dispName = "Flak Shot";
		desc = "Fire a volley of bullets.";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityFlakShot", typeof(Sprite));
		cost = 0;
		cooldown = 1f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new FlakShot (invoker, bulletPrefab);
	}
		
	public override bool use(){
		for(int i = 0; i < 5; i++){
			Quaternion bulletRot = Quaternion.Euler(new Vector3(0, 0, invoker.eulerAngles.z + (5f * i) - 10));
			Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, bulletRot);
		}
			
		currentCD = cooldown;
		return false; 
	}
}
