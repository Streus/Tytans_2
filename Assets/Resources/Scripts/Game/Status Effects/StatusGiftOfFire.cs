using UnityEngine;
using System.Collections;

public class StatusGiftOfFire : StatusEffect
{
	private GameObject[] bullets;

	public StatusGiftOfFire(float dur, Transform e) : base(dur, e)
	{
		bullets = new GameObject[4];
	}

	public override StatusEffect Copy (Transform e)
	{
		return new StatusGiftOfFire (initDuration, invoker);
	}

	public override void apply ()
	{
		//add bullets
		for (int i = 0; i < bullets.Length; i++)
		{
			bullets [i] = Bullet.createBullet (invoker, Resources.Load<GameObject> ("Prefabs/Bullets/BulletGiftOfFire"), invoker.position, invoker.rotation);
			bullets [i].transform.localRotation = Quaternion.Euler (0f, 0f, 90f * (float)i);
		}
	}

	public override void revert ()
	{
		//remove bullets
		for (int i = 0; i < bullets.Length; i++)
		{
			if(bullets[i] != null)
				bullets [i].GetComponent<CirclingBullet> ().duration = 0;
		}
	}
}
