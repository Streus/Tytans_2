using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		loadOptions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Load all options out of PlayerPrefs and save them in global values
	public void loadOptions()
	{
		// Bindings
		Bindings.move = (KeyCode)PlayerPrefs.GetInt("keymove", (int)KeyCode.Mouse0);
		Bindings.dash = (KeyCode)PlayerPrefs.GetInt("keydash", (int)KeyCode.Space);
		Bindings.hold = (KeyCode)PlayerPrefs.GetInt("keyhold", (int)KeyCode.LeftShift);
		Bindings.toggleInventory = (KeyCode)PlayerPrefs.GetInt("keyinventory", (int)KeyCode.Tab);
		Bindings.pause = (KeyCode)PlayerPrefs.GetInt("keypause", (int)KeyCode.Escape);
		Bindings.ability0 = (KeyCode)PlayerPrefs.GetInt("keyability0", (int)KeyCode.Mouse1);
		Bindings.ability1 = (KeyCode)PlayerPrefs.GetInt("keyability1", (int)KeyCode.Q);
		Bindings.ability2 = (KeyCode)PlayerPrefs.GetInt("keyability2", (int)KeyCode.W);
		Bindings.ability3 = (KeyCode)PlayerPrefs.GetInt("keyability3", (int)KeyCode.E);
		Bindings.ability4 = (KeyCode)PlayerPrefs.GetInt("keyability4", (int)KeyCode.R);
		Bindings.ability5 = (KeyCode)PlayerPrefs.GetInt("keyability5", (int)KeyCode.T);

		// Options
		Options.tutorial = bool.Parse(PlayerPrefs.GetString("optiontutorial", "true"));
	}

	// Save all of the global options values to PlayerPrefs
	public void saveOptions()
	{
		// Bindings
		PlayerPrefs.SetInt("keymove", (int)Bindings.move);
		PlayerPrefs.SetInt("keydash", (int)Bindings.dash);
		PlayerPrefs.SetInt("keyhold", (int)Bindings.hold);
		PlayerPrefs.SetInt("keyinventory", (int)Bindings.toggleInventory);
		PlayerPrefs.SetInt("keypause", (int)Bindings.pause);
		PlayerPrefs.SetInt("keyability0", (int)Bindings.ability0);
		PlayerPrefs.SetInt("keyability1", (int)Bindings.ability1);
		PlayerPrefs.SetInt("keyability2", (int)Bindings.ability2);
		PlayerPrefs.SetInt("keyability3", (int)Bindings.ability3);
		PlayerPrefs.SetInt("keyability4", (int)Bindings.ability4);
		PlayerPrefs.SetInt("keyability5", (int)Bindings.ability5);

		// Options
		PlayerPrefs.SetString("optiontutorial", Options.tutorial.ToString());
	}

	public void resetControlsToDefaults()
	{
		Bindings.move = KeyCode.Mouse0;
		Bindings.dash = KeyCode.Space;
		Bindings.hold = KeyCode.LeftShift;
		Bindings.toggleInventory = KeyCode.Tab;
		Bindings.pause = KeyCode.Escape;
		Bindings.ability0 = KeyCode.Mouse1;
		Bindings.ability1 = KeyCode.Q;
		Bindings.ability2 = KeyCode.W;
		Bindings.ability3 = KeyCode.E;
		Bindings.ability4 = KeyCode.R;
		Bindings.ability5 = KeyCode.T;
	}
}

// A global variable class that holds all key bindings
public static class Bindings
{
	public static KeyCode 
	move,
	dash,
	hold, 
	toggleInventory,
	pause, 
	ability0,
	ability1,
	ability2,
	ability3,
	ability4,
	ability5;
}
	
// A global variable class that holds unique, non-default options
public static class Options
{
	//difficulty will only be loaded/saved on a per save-file basis
	public static Difficulty difficulty;

	public static bool tutorial;
}

public enum Difficulty{ Easy, Normal, Hard }