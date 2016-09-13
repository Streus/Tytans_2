using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityChooseButton : MonoBehaviour {

	public Image image;
	public Ability ability;
	public int slotNumber;

	// Use this for initialization
	void Start () {
		image = transform.GetComponent<Image>();
		ability = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeAbility()
	{
		GameManager.player.GetComponent<Entity>().addAbility(ability, slotNumber);
	}
}
