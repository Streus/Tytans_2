using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleTutorialButton : MonoBehaviour {

	private Toggle toggle;

	// Use this for initialization
	void Awake () {
		toggle = transform.GetComponent<Toggle>();
	}
	void Start () {
		toggle.isOn = Options.tutorial;
	}
	
	public void toggleValue()
	{
		Options.tutorial = toggle.isOn;
	}
}
