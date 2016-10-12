using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CyclingDeathTaunt : MonoBehaviour {

	private Text tauntText;

	// Use this for initialization
	void Awake () {
		tauntText = transform.GetChild (0).GetComponent<Text> ();
		rollNewTaunt ();
	}

	public void rollNewTaunt()
	{
		float numTaunts = 5f; //actual amount will be +1
		int chosenTaunt = (int)(Random.value * numTaunts);
		string taunt = "";
		switch (chosenTaunt) {
		case 0:
			taunt = "You didn't last as long as I thought you would.";
			break;
		case 1:
			taunt = "Do you need a bigger ship?";
			break;
		case 2:
			taunt = "You can pull out your hair later; get back in there!";
			break;
		case 3:
			taunt = "If you have time to die, you have time to...not die.";
			break;
		case 4:
			taunt = "Oh, I agree; that was totally unfair.  You should still try again, though.";
			break;
		case 5:
			taunt = "You call that trying?";
			break;
		}
		tauntText.text = taunt;
	}
}
