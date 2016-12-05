using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class HeadsUpManager : MonoBehaviour 
{
	// Entity to pull stats from
	private Entity player;

	private Image healthBarBack;
	private Image healthBar;
	private GameObject healthBarGO;
	private Image shieldBar;
	private Image heatBarBack;
	private Image heatBar;
	private Transform statusBar;
	private Transform abilityBar;

	// Use this for initialization
	void Start ()
	{
		player = GameManager.player.transform.GetComponent<Entity>();

		//add a listener to the changedStatuses event
		player.changedStatuses += new UpdatedStatusList (addStatusToStatusBar);

		healthBarBack = transform.GetChild (0).GetChild (0).GetComponent<Image> ();
		healthBarGO = transform.GetChild (0).GetChild (1).gameObject;
		healthBar = healthBarGO.GetComponent<Image> ();
		shieldBar = transform.GetChild (0).GetChild (2).GetComponent<Image> ();
		heatBarBack = transform.GetChild (1).GetChild (0).GetComponent<Image> ();
		heatBar = transform.GetChild (1).GetChild (1).GetComponent<Image> ();
		statusBar = transform.GetChild (2).GetChild (0);
		abilityBar = transform.GetChild (2).GetChild (1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//check for destroyed player
		if (GameManager.player == null)
			return;

		//update health and heat bars
		float percentHP = player.health / player.healthMax;
		healthBar.fillAmount = percentHP;

		healthBarGO.GetComponent<Animator> ().SetBool ("IsCritical", percentHP <= 0.35f);

		if(player.shieldMax != 0)
			shieldBar.fillAmount = player.shieldHealth / player.shieldMax;
		else
			shieldBar.fillAmount = 0;

		float percentHeat = player.heat / player.heatMax;
		heatBar.fillAmount = percentHeat;
		heatBar.color = new Color (percentHeat, 1 - percentHeat, 1f);

		//update resource bar backgrounds
		if(healthBarBack.fillAmount != healthBar.fillAmount)
		{
			float dFill = healthBar.fillAmount - healthBarBack.fillAmount;
			healthBarBack.fillAmount += dFill * Time.deltaTime;
		}

		if(heatBarBack.fillAmount != heatBar.fillAmount)
		{
			float dFill = heatBar.fillAmount - heatBarBack.fillAmount;
			heatBarBack.fillAmount += dFill * Time.deltaTime;
		}

		//update ability bar
		for (int i = 0; i < player.abilities.Length; i++) {
			abilityBar.GetChild (i).GetComponent<AbilitySlot> ().ability = player.abilities [i];
		}
	}

	void OnDestroy ()
	{
		//remove this object's listener to the changedStatuses event
		player.changedStatuses -= new UpdatedStatusList (addStatusToStatusBar);
	}

	// Add a GUI GameObject to the status bar to represent the passed status effect
	private void addStatusToStatusBar(StatusEffect status)
	{
		GameObject statusGUI = (GameObject)Instantiate(Resources.Load<GameObject> ("Prefabs/UI/StatusEffect"));
		statusGUI.transform.SetParent (statusBar.transform, false);
		StatusListener statusGUIData = statusGUI.GetComponent<StatusListener> ();
		statusGUIData.setStatus (status);
	}
}
