using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitySelectorButton : MonoBehaviour {

	public Image abilityGraphic;
	private GameObject slotList;
	public Ability slotValue;

	// Use this for initialization
	void Awake () {
		//hide the dropdown
		slotList = transform.GetChild(1).gameObject;
		slotList.SetActive(false);

		abilityGraphic = transform.GetChild(0).GetComponent<Image> ();
	}

	public void updateGraphic()
	{
		abilityGraphic.sprite = slotValue.image;
	}

	public void toggleListActive()
	{
		slotList.SetActive (!slotList.activeSelf);
	}
}