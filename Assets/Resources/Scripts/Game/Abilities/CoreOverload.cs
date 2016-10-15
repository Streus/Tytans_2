using UnityEngine;
using System.Collections;

public class CoreOverload : Ability {
	
	public CoreOverload(Transform e) : base(e){ }
	public CoreOverload() : base(){ }

	protected override void setValues ()
	{
		dispName = "Core Overload";
		desc = "Emit a powerful shockwave that knocks back and damages enemies as well as restoring some energy.";
		image = (Sprite)Resources.Load<Sprite>("Sprites/UI/Abilities/AbilityCoreOverload");
		cost = -80f;
		cooldown = 10f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new CoreOverload (invoker);
	}
		
	public override bool use(){
		//explosion boom boom
		GameObject shockBullet = Resources.Load<GameObject>("Prefabs/Bullets/BulletSpark");
		for(int i = 0; i < 30; i++){
			Quaternion bulletRot = Quaternion.Euler(new Vector3(0, 0, invoker.eulerAngles.z + (12f * i)));
			GameObject b = (GameObject)MonoBehaviour.Instantiate(shockBullet, invoker.position, bulletRot);
			Physics2D.IgnoreCollision(b.transform.GetComponent<Collider2D>(), invoker.GetComponent<Collider2D>());
			Bullet bullet = b.transform.GetComponent<Bullet>();
			bullet.faction = invoker.GetComponent<Entity>().faction;
		}

		//apply regen and reset ability
		invoker.GetComponent<Entity>().addStatus(new StatusENRegen(2f, invoker, 40f));
		currentCD = cooldown;
		return true; 
	}
}
