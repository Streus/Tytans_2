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
		case KeyName.forward: return "Forward";
		case KeyName.strafeL: return "Strafe Left";
		case KeyName.reverse: return "Reverse";
		case KeyName.strafeR: return "Strafe Right";
		case KeyName.fire: return "Fire";
		case KeyName.classAbility: return "Class Ability";
		case KeyName.ability0: return "Ability 1";
		case KeyName.ability1: return "Ability 2";
		case KeyName.ability2: return "Ability 3";
		case KeyName.toggleInventory: return "Inventory";
		case KeyName.pause: return "Pause";
		default: return "UNKNOWN";
		}
	}

	public void setKey()
	{
		switch(keyTitle){
		case KeyName.forward: key = Bindings.forward; break;
		case KeyName.strafeL: key = Bindings.strafeL; break;
		case KeyName.reverse: key = Bindings.reverse; break;
		case KeyName.strafeR: key = Bindings.strafeR; break;
		case KeyName.fire: key = Bindings.fire; break;
		case KeyName.classAbility: key = Bindings.classAbility; break;
		case KeyName.ability0: key = Bindings.ability0; break;
		case KeyName.ability1: key = Bindings.ability1; break;
		case KeyName.ability2: key = Bindings.ability2; break;
		case KeyName.toggleInventory: key = Bindings.toggleInventory; break;
		case KeyName.pause: key = Bindings.pause; break;
		}
	}

	public void updateBinding()
	{
		switch(keyTitle){
		case KeyName.forward: Bindings.forward = key; break;
		case KeyName.strafeL: Bindings.strafeL = key; break;
		case KeyName.reverse: Bindings.reverse = key; break;
		case KeyName.strafeR: Bindings.strafeR = key; break;
		case KeyName.fire: Bindings.fire = key; break;
		case KeyName.classAbility: Bindings.classAbility = key; break;
		case KeyName.ability0: Bindings.ability0 = key; break;
		case KeyName.ability1: Bindings.ability1 = key; break;
		case KeyName.ability2: Bindings.ability2 = key; break;
		case KeyName.toggleInventory: Bindings.toggleInventory = key; break;
		case KeyName.pause: Bindings.pause = key; break;
		}
	}
}

public enum KeyName 
{
	forward,
	strafeL,
	reverse,
	strafeR,
	fire,
	classAbility,
	ability0,
	ability1,
	ability2,
	toggleInventory,
	pause
}