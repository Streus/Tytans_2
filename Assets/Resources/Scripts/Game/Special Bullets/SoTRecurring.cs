using UnityEngine;
using System.Collections;

public class SoTRecurring : Bullet {

	private GameObject blade;

	public override void Start () {
		base.Start ();

		GameObject bulPrefab = Resources.Load<GameObject> ("Prefabs/Bullets/BulletSoTMidsection");
		blade = Bullet.createBullet (creator, bulPrefab, transform.position, transform.rotation);
	}

	public override void Update() {
		base.Update ();

		blade.transform.localScale = new Vector3(1, blade.transform.localScale.y + 0.5f, 1);
	}
}
