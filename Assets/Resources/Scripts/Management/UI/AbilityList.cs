using UnityEngine;
using System.Collections;

public class AbilityList : MonoBehaviour {

	private bool isOpen;

	// Use this for initialization
	void Start () {
		isOpen = false;
	}
	
	public void toggleList()
	{
		if (isOpen)
			destroyAllChildren ();
		else
			buildList ();
		isOpen = !isOpen;
	}

	private void buildList()
	{
		ArrayList playerAbilities = GameManager.player.GetComponent<Player> ().learnedAbilities;
		for (int i = 0; i < playerAbilities.Count; i++) 
		{
			GameObject newSlot = (GameObject)Instantiate (Resources.Load<GameObject> ("Prefabs/UI/AbilitySelector"));
			newSlot.transform.SetParent (transform, false);
			newSlot.GetComponent<AbilitySelectorButton> ().slotValue = (Ability)playerAbilities [i];
			newSlot.GetComponent<AbilitySelectorButton> ().updateGraphic ();
		}
	}

	private void destroyAllChildren()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
