using UnityEngine;
using System.Collections;

public class Justice : BulletFlexAbility {

	private bool stage;

	public Justice() : base() { }
	public Justice(Transform e, GameObject bullPre) : base(e, bullPre) { }

	protected override void setValues ()
	{
		dispName = "Justice";
		desc = "Fire two volleys of bullets in succession.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityJustice");
		cost = 0;
		cooldown = 1.5f;
		currentCD = cooldown;

		stage = false;
	}

	public override Ability Copy ()
	{
		return new Justice (invoker, bulletPrefab);
	}

	public override void use ()
	{
		if (!stage) 
		{
			//forward spread
			for (int i = 0; i < 5; i++) {
				Quaternion bulletRot = Quaternion.Euler (new Vector3 (0, 0, invoker.eulerAngles.z + (5f * i) - 10));
				Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, bulletRot);
			}
			stage = true;
			currentCD = 1f / ((float)((int)GameManager.manager.difficulty + 1));
		} 
		else 
		{
			//leftside spread
			for (int i = 0; i < 5; i++) {
				Quaternion bulletRot = Quaternion.Euler (new Vector3 (0, 0, invoker.eulerAngles.z + (5f * i) - 40));
				Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, bulletRot);
			}

			//rightside spread
			for (int i = 0; i < 5; i++) {
				Quaternion bulletRot = Quaternion.Euler (new Vector3 (0, 0, invoker.eulerAngles.z + (5f * i) + 20));
				Bullet.createBullet (invoker.gameObject, bulletPrefab, invoker.position, bulletRot);
			}

			stage = false;
			currentCD = cooldown;
		}
	}
}
