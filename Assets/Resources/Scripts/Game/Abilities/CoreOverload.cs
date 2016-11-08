using UnityEngine;
using System.Collections;

public class CoreOverload : Ability {
	
	public CoreOverload(Transform e) : base(e){ }
	public CoreOverload() : base(){ }

	protected override void setValues ()
	{
		dispName = "Core Overload";
		desc = "Emit a powerful shockwave that damages enemies as well as restoring 80 energy.";
		image = (Sprite)Resources.Load<Sprite>("Sprites/UI/Abilities/AbilityCoreOverload");
		cost = 0;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new CoreOverload (invoker);
	}
		
	public override void use(){
		base.use ();

		//explosion boom boom
		GameObject shockBullet = Resources.Load<GameObject>("Prefabs/Bullets/BulletSpark");
		for(int i = 0; i < 30; i++){
			Quaternion bulletRot = Quaternion.Euler(new Vector3(0, 0, invoker.eulerAngles.z + (12f * i)));
			Bullet.createBullet (invoker.gameObject, shockBullet, invoker.position, bulletRot);
		}

		//apply regen and reset ability
		invoker.GetComponent<Entity>().addStatus(new StatusENRegen(2f, invoker, 40f));
	}
}
