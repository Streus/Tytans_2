using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour {

	public Ability ability;
	private Image icon;
	private Image cdIndicator;
	private Text cdText;

	// Use this for initialization
	void Start () {
		icon = transform.GetChild (0).GetComponent<Image> ();
		cdIndicator = transform.GetChild (1).GetComponent<Image> ();
		cdText = transform.GetChild (2).GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ability != null) {
			icon.sprite = ability.image;
			cdIndicator.fillAmount = ability.currentCD / ability.cooldown;
			cdText.text = (ability.currentCD).ToString("##.#");
		}
	}

	//TODO mouseover information?
}
