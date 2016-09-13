using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitySelectorButton : MonoBehaviour {

	private Image abilityGraphic;
	private GameObject slotList;
	private Transform content;
	public Ability slotValue;
	public int slotNumber;

	// Use this for initialization
	void Start () {
		//hide the dropdown
		slotList = transform.GetChild(1).gameObject;
		slotList.SetActive(false);

		// get the content window
		content = slotList.transform.GetChild(0).GetChild(0).transform;

		//set the graphic to the given ability, if there is one.
		abilityGraphic = transform.GetChild(0).GetChild(0).GetComponent<Image>();
		if(slotValue != null)
			abilityGraphic.sprite = slotValue.image;
		else
			abilityGraphic.color = Color.clear;	
	}

	public void toggleListActive()
	{
		// toggle the activity of the selection list
		slotList.SetActive(!slotList.activeSelf);

		if(slotList.activeSelf){
			ArrayList abils = GameManager.player.GetComponent<Player>().learnedAbilities;

			for(int i = 0; i < abils.Count; i++){
				GameObject abilitySlot = Instantiate(Resources.Load<GameObject>("Prefabs/UI/AbilityChooseButton"));
				AbilityChooseButton acb = abilitySlot.GetComponent<AbilityChooseButton>();
				acb.ability = (Ability)abils[i];
				acb.image.sprite = acb.ability.image;
				acb.slotNumber = slotNumber;
				abilitySlot.transform.SetParent(content, false);
				RectTransform rt = content.GetComponent<RectTransform>();
				rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 100f);
			}
		}
		else{
			foreach(Transform child in transform)
				if(child != transform)
					Destroy(child.gameObject);
		}
	}
}