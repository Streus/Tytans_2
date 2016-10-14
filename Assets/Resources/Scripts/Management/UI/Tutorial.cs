using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	private Text textBox;
	private Transform arrow;
	public GameObject spawnTrigger;

	private int state;
	private KeyCode targetKey;
	private float timerDuration;

	// Use this for initialization
	void Start () {
		if (!Options.tutorial) {
			endTutorial ();
		}

		textBox = transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		arrow = transform.GetChild (1);
		arrow.gameObject.SetActive (false);
		textBox.text = "Comfortable?  Good.  Let's get started...";
		timerDuration = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		timerDuration -= Time.deltaTime;
		if (Input.GetKeyDown (targetKey) || timerDuration <= 0f)
			updateState ();
	}

	public void updateState()
	{
		state++;
		switch (state) {
		case 1:
			textBox.text = "Let's start with the essentials.\nPress <color=#ff7700><b>" + Bindings.forward.ToString () + "</b></color> to move toward the mouse.";
			targetKey = Bindings.forward;
			timerDuration = float.PositiveInfinity;
			break;
		case 2:
			textBox.text = "I'm glad you're not a complete idiot.  Now do that in reverse.\nPress <color=#ff7700><b>" + Bindings.reverse.ToString () + "</b></color>.";
			targetKey = Bindings.reverse;
			break;
		case 3:
			textBox.text = "That's not all this beauty can do.\nTry <color=#ff7700><b>" + Bindings.strafeL.ToString () + "</b></color> to strafe left.";
			targetKey = Bindings.strafeL;
			break;
		case 4:
			textBox.text = "Can you guess what's next?  Yup.\nUse <color=#ff7700><b>" + Bindings.strafeR.ToString () + "</b></color> to strafe right.";
			targetKey = Bindings.strafeR;
			break;
		case 5:
			textBox.text = "So that's movement out of the way...";
			targetKey = KeyCode.None;
			timerDuration = 3f;
			break;
		case 6:
			textBox.text = "Oh yea, you should probably also know how to fight.\nPress <color=#ff7700><b>" + Bindings.fire.ToString () + "</b></color> to use your primary ability.";
			targetKey = Bindings.fire;
			timerDuration = float.PositiveInfinity;
			//arrow pointing at ability bar?
			break;
		case 7:
			textBox.text = "That'll be your bread.\nUse <color=#ff7700><b>" + Bindings.classAbility.ToString () + "</b></color> to try out your butter.";
			GameManager.player.GetComponent<Entity> ().abilities [1].currentCD = 0f;
			targetKey = Bindings.classAbility;
			break;
		case 8:
			textBox.text = "And don't worry, you can also have three additional abilities.\nYou can use them with <color=#ff7700><b>"
			+ Bindings.ability0.ToString () + "</b></color>"
			+ ", <color=#ff7700><b>" + Bindings.ability1.ToString () + "</b></color>"
			+ ", and <color=#ff7700><b>" + Bindings.ability2.ToString () + "</b></color>.";
			targetKey = KeyCode.None;
			timerDuration = 10f;
			break;
		case 9:
			textBox.text = "You can switch those out at any time via your inventory.\n" +
			"The inventory is opened with either the button to the right of\n" +
			"the screen, or by pressing <color=#ff7700><b>" + Bindings.toggleInventory.ToString () + "</b></color>.";
			timerDuration = 10f;
			break;
		case 10:
			textBox.text = "Well, you survived the tutorial.  I suppose that's a good sign.\n" +
			"I guess I'll set you loose then.  Just move into the circle\n" +
			"whenever you're ready.";
			timerDuration = 10f;
			break;
		case 11:
			endTutorial ();
			break;
		}
	}

	private void endTutorial()
	{
		//disable tutorial option
		Options.tutorial = false;

		//activate boss spawning circle
		spawnTrigger.GetComponent<BossSpawnTrigger>().armed = true;

		//move out of the tutorial
		MenuManager.menuSystem.showMenu (MenuManager.menuSystem.getMenu ("Empty"));
	}
}
