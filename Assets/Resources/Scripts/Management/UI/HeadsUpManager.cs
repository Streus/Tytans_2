using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadsUpManager : MonoBehaviour {

	// Entity to pull stats from
	public Entity player;

	private Image healthBar;
	private Image energyBar;
	private Transform statusBar;
	private Transform abilityBar;

	// Use this for initialization
	void Start () {
		healthBar = transform.GetChild (0).GetChild (0).GetComponent<Image> ();
		energyBar = transform.GetChild (1).GetChild (0).GetComponent<Image> ();
		statusBar = transform.GetChild (2).GetChild (0);
		abilityBar = transform.GetChild (2).GetChild (1);
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = player.health / player.healthMax;
		energyBar.fillAmount = player.energy / player.energyMax;
	}
}
