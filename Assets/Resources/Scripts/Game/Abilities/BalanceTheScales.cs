using UnityEngine;
using System.Collections;

public class BalanceTheScales : Ability {

	public BalanceTheScales(Transform e) : base(e){ }
	public BalanceTheScales() : base(){ }

	protected override void setValues ()
	{
		dispName = "Balance the Scales";
		desc = "Apply your status effects to nearby enemies";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityBalanceTheScales");
		cost = 25f;
		cooldown = 20f;
		currentCD = cooldown;
	}

	public override Ability Copy ()
	{
		return new BalanceTheScales (invoker);
	}

	public override bool use ()
	{
		GameObject bp = Resources.Load<GameObject> ("Prefabs/Bullets/BtSWave");
		Bullet.createBullet (invoker.gameObject, bp, invoker.position, invoker.rotation);

		invoker.GetComponent<Entity> ().energy -= cost;
		currentCD = cooldown;
		return true;
	}
}
