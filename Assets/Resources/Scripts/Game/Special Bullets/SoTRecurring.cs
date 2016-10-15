using UnityEngine;
using System.Collections;

public class SoTRecurring : MonoBehaviour {

	private Bullet bulScr;

	private GameObject blade;

	// Use this for initialization
	void Start () {
		bulScr = transform.GetComponent<Bullet> ();

		GameObject bulPrefab = Resources.Load<GameObject> ("Prefabs/Bullets/BulletSoTMidsection");
		blade = (GameObject)MonoBehaviour.Instantiate(bulPrefab, transform.position, transform.rotation);
		Bullet bullet = blade.transform.GetComponent<Bullet>();
		bullet.faction = bulScr.faction;
	}

	void Update() {
		blade.transform.localScale = new Vector3(1, blade.transform.localScale.y + 0.5f, 1);
	}
}
