using UnityEngine;
using System.Collections;

public class ExplosiveBullet : Bullet {

	public GameObject explosion;

	protected override void die ()
	{
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
