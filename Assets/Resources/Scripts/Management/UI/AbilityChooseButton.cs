using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityChooseButton : MonoBehaviour {

	public Ability ability;
	public int slotNumber;

	// Use this for initialization
	void Start () {
		ability = transform.parent.parent.GetComponent<AbilitySelectorButton>().slotValue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeAbility()
	{
		//check for dublicate abilities in the player's roster
		Ability[] abilities = GameManager.player.GetComponent<Entity> ().abilities;
		for (int i = 0; i < abilities.Length; i++) 
		{
			if (abilities [i] == null)
				continue;
			if (abilities [i].CompareTo (ability) == 0)
				return;
		}

		//assign a copy of this ability to the player's roster
		GameManager.player.GetComponent<Entity>().addAbility(ability.Copy(), slotNumber);
	}
}
