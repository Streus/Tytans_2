using UnityEngine;
using System.Collections;

public class ExplosionBullet : Bullet {

	public override void Start ()
	{
		base.Start ();

		GameManager.cameraController.shakeCamera (0.1f, 0.2f);
	}
}
