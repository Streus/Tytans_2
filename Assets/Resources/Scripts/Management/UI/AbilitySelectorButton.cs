using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitySelectorButton : MonoBehaviour {

	private Image abilityGraphic;
	private GameObject slotList;
	public Ability slotValue;

	// Use this for initialization
	void Start () {
		//hide the dropdown
		slotList = transform.GetChild(0).gameObject;
		slotList.SetActive(false);

		//set the graphic to the given ability, if there is one.
		abilityGraphic = transform.GetChild(1).GetComponent<Image>();
		if(slotValue != null)
			abilityGraphic.sprite = slotValue.image;
		else
			abilityGraphic.color = Color.clear;
			
	}

	public void toggleListActive()
	{
		slotList.SetActive(!slotList.activeSelf);
	}
}
