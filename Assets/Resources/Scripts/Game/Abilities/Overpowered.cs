using UnityEngine;
using System.Collections;

public class Overpowered : Ability 
{
	public Overpowered(Transform e) : base(e){ }
	public Overpowered() : base(){ }

	protected override void setValues ()
	{
		dispName = "Overpowered";
		desc = "Fire a number of powerful bullets equal to the number\nof charges of this ability.";
		image = Resources.Load<Sprite> ("Sprites/UI/Abilities/AbilityOverpowered");
		cost = 15f;
		cooldown = 3f;
		currentCD = cooldown;

		maxCharges = 40;
	}

	public override Ability Copy ()
	{
		return new Overpowered (invoker);
	}

	public override void use ()
	{
		invoker.GetComponent<Entity> ().energy -= cost;

		GameObject bulPrefab = Resources.Load<GameObject> ("Prefabs/Bullets/BulletGolden");
		for (int i = 0; i < currentCharges; i++)
		{
			Quaternion rot = Quaternion.Euler (0f, 0f, invoker.rotation.eulerAngles.z - (4.5f * currentCharges) + (i * 9f));
			Bullet.createBullet (invoker.gameObject, bulPrefab, invoker.position, rot);
		}

		currentCharges = 0;
		currentCD = cooldown;
	}
}
