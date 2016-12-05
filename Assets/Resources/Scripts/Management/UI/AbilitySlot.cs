using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
	public Ability ability;
	private Image icon;
	private Image cdIndicator;
	private Text cdText;
	private GameObject chargeIndicator;
	private Text chargeText;

	private GameObject popupInfo;

	// Use this for initialization
	void Start () 
	{
		icon = transform.GetChild (0).GetComponent<Image> ();
		cdIndicator = transform.GetChild (1).GetComponent<Image> ();
		cdText = transform.GetChild (2).GetComponent<Text> ();
		chargeIndicator = transform.GetChild (3).gameObject;
		chargeText = chargeIndicator.transform.GetChild(0).GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ability != null) 
		{
			icon.sprite = ability.image;

			cdIndicator.fillAmount = ability.currentCD / ability.cooldown;
			cdText.text = (ability.currentCD).ToString ("###.#");

			chargeIndicator.SetActive (ability.maxCharges > 0);
			if(chargeIndicator.activeSelf)
				chargeText.text = ability.currentCharges.ToString ("###");
		} 
		else 
		{
			icon.sprite = null;
			cdIndicator.fillAmount = 0;
			cdText.text = "";
			chargeIndicator.SetActive (false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (ability != null)
		{
			popupInfo = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/DescriptionBox"), transform, false);
			popupInfo.transform.GetComponent<RectTransform> ().offsetMin += new Vector2 (0f, 50f);
			popupInfo.transform.GetChild (0).GetComponent<Text> ().text = ability.ToString ();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(popupInfo != null)
			Destroy(popupInfo);
	}
}
