using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthDisplay : MonoBehaviour
{
	public static BossHealthDisplay pool;

	public ArrayList bosses;

	// Use this for initialization
	void Start ()
	{
		if (pool == null)
			pool = this;
		else if(pool != this)
			Destroy (gameObject);

		bosses = new ArrayList ();
	}

	// Add the passed Entity and a health bar to the pool 
	public void createNewHealthBar(Entity boss)
	{
		GameObject newBar = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/Bar Graphic"), transform, false);
		newBar.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = boss.gameObject.name;
		bosses.Add (boss);
	}

	// Remove the passed Entity and a health bar from the pool
	public void removeHealthBar(Entity boss)
	{
		bosses.Remove (boss);
		Destroy (transform.GetChild (0).gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < bosses.Count; i++)
		{
			Entity boss = (Entity)bosses [i];
			float hppercent = boss.health / boss.healthMax;
			float spercent = boss.shieldHealth / boss.shieldMax;
			transform.GetChild (i).GetComponent<Image> ().fillAmount = hppercent;
			transform.GetChild (i).GetChild(0).GetComponent<Image> ().fillAmount = spercent;
		}
	}
}
