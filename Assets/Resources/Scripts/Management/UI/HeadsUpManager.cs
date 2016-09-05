using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class HeadsUpManager : MonoBehaviour {

	// Entity to pull stats from
	private Entity player;

	private Image healthBar;
	private Image energyBar;
	private Transform statusBar;
	private Transform abilityBar;

	// Use this for initialization
	void Start () {
		player = GameManager.player.transform.GetComponent<Entity>();

		//add a listener to the changedStatuses event
		player.changedStatuses += new UpdatedStatusList (addStatusToStatusBar);

		healthBar = transform.GetChild (0).GetChild (0).GetComponent<Image> ();
		energyBar = transform.GetChild (1).GetChild (0).GetComponent<Image> ();
		statusBar = transform.GetChild (2).GetChild (0);
		abilityBar = transform.GetChild (2).GetChild (1);
	}
	
	// Update is called once per frame
	void Update () {
		//update health and energy bars
		healthBar.fillAmount = player.health / player.healthMax;
		energyBar.fillAmount = player.energy / player.energyMax;

		//update ability bar
		for (int i = 0; i < player.abilities.Length; i++) {
			abilityBar.GetChild (i).GetComponent<AbilitySlot> ().ability = player.abilities [i];
		}
	}

	void OnDestroy () {
		//remove this object's listener to the changedStatuses event
		player.changedStatuses -= new UpdatedStatusList (addStatusToStatusBar);
	}

	// Add a GUI GameObject to the statsu bar to represent the passed status effect
	private void addStatusToStatusBar(StatusEffect status)
	{
		GameObject statusGUI = (GameObject)Instantiate(Resources.Load<GameObject> ("Prefabs/UI/StatusEffect"), statusBar.transform);
		StatusListener statusGUIData = statusGUI.GetComponent<StatusListener> ();
		statusGUIData.status = status;
	}
}
