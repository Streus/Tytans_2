using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthDisplay : MonoBehaviour {

	public static BossHealthDisplay pool;

	// Use this for initialization
	void Start () {
		if (pool == null)
			pool = this;
		else if(pool != this)
			Destroy (gameObject);
	}

	public void createNewHealthBar(Entity boss)
	{
		GameObject newBar = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/Bar Graphic"));
		newBar.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = boss.gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
