using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RebindButton : MonoBehaviour {

	private KeyCode key;
	public KeyName keyTitle;
	private Text nameField;
	private Text valueField;
	private Button rebindButton;

	private bool rebinding;

	// Use this for initialization
	void Start () {
		nameField = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		valueField = transform.GetChild(1).GetChild(0).GetComponent<Text>();
		rebindButton = transform.GetChild(1).GetComponent<Button>();

		nameField.text = getBindingName();
		setKey();
		valueField.text = key.ToString();

		rebinding = false;
	}
	
	// Update is called once per frame
	void Update () {

		//check if in rebinding mode
		if (rebinding && Input.anyKeyDown) 
		{
			foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
				if(Input.GetKey(vKey))
					key = vKey;
			}
			updateBinding();
			rebinding = false;
		}

		//update the displayed key value
		setKey();
		valueField.text = key.ToString();

		//enable/disable the rebind button
		rebindButton.interactable = !rebinding;
	}

	public void readyRebind()
	{
		rebinding = true;
		key = KeyCode.None;
	}

	private string getBindingName()
	{
		switch(keyTitle){
		case KeyName.move: return "Move";
		case KeyName.classAbility: return "Class Ability";
		case KeyName.hold: return "Hold";
		case KeyName.toggleInventory: return "Inventory";
		case KeyName.pause: return "Pause";
		case KeyName.ability0: return "Ability 1";
		case KeyName.ability1: return "Ability 2";
		case KeyName.ability2: return "Ability 3";
		case KeyName.ability3: return "Ability 4";
		case KeyName.ability4: return "Ability 5";
		case KeyName.ability5: return "Ability 6";
		default: return "UNKNOWN";
		}
	}

	public void setKey()
	{
		switch(keyTitle){
		case KeyName.move: key = Bindings.move; break;
		case KeyName.classAbility: key = Bindings.classAbility; break;
		case KeyName.hold: key = Bindings.hold; break;
		case KeyName.toggleInventory: key = Bindings.toggleInventory; break;
		case KeyName.pause: key = Bindings.pause; break;
		case KeyName.ability0: key = Bindings.ability0; break;
		case KeyName.ability1: key = Bindings.ability1; break;
		case KeyName.ability2: key = Bindings.ability2; break;
		case KeyName.ability3: key = Bindings.ability3; break;
		case KeyName.ability4: key = Bindings.ability4; break;
		case KeyName.ability5: key = Bindings.ability5; break;
		}
	}

	public void updateBinding()
	{
		switch(keyTitle){
		case KeyName.move: Bindings.move = key; break;
		case KeyName.classAbility: Bindings.classAbility = key; break;
		case KeyName.hold: Bindings.hold = key; break;
		case KeyName.toggleInventory: Bindings.toggleInventory = key; break;
		case KeyName.pause: Bindings.pause = key; break;
		case KeyName.ability0: Bindings.ability0 = key; break;
		case KeyName.ability1: Bindings.ability1 = key; break;
		case KeyName.ability2: Bindings.ability2 = key; break;
		case KeyName.ability3: Bindings.ability3 = key; break;
		case KeyName.ability4: Bindings.ability4 = key; break;
		case KeyName.ability5: Bindings.ability5 = key; break;
		}
	}
}

public enum KeyName 
{
	move,
	classAbility,
	hold,
	toggleInventory,
	pause,
	ability0,
	ability1,
	ability2,
	ability3,
	ability4,
	ability5
}