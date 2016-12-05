using UnityEngine;
using System.Collections;

public class BurstShot : BulletFlexAbility {
	
	private int shotNumber;

	public BurstShot(Transform e, GameObject bt) : base(e, bt){
		shotNumber = 1;
	}
	public BurstShot() : base(){ }

	protected override void setValues ()
	{
		dispName = "Burst Shot";
		image = (Sprite)Resources.Load ("Sprites/UI/Abilities/AbilityBurstShot", typeof(Sprite));
		cost = -1f;
		cooldown = 1f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new BurstShot (invoker, bulletPrefab);
	}
		
	public override void use()
	{
		invoker.GetComponent<Entity> ().heat += cost;

		Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, invoker.rotation);

		shotNumber = shotNumber % 3;
		if (shotNumber == 0)
			currentCD = cooldown;
		else
			currentCD = 0.05f;
		shotNumber++;
	}
}
