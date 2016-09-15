using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

	public AudioMixer mainAudio;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		loadOptions();
		PlayerPrefs.DeleteAll (); // DEBUG CODE
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Load all options out of PlayerPrefs and save them in global values
	public void loadOptions()
	{
		// Bindings
		Bindings.forward = (KeyCode)PlayerPrefs.GetInt("keyforward", (int)KeyCode.W);
		Bindings.strafeL = (KeyCode)PlayerPrefs.GetInt ("keystrafel", (int)KeyCode.A);
		Bindings.reverse = (KeyCode)PlayerPrefs.GetInt("keyreverse", (int)KeyCode.S);
		Bindings.strafeR = (KeyCode)PlayerPrefs.GetInt ("keystrafer", (int)KeyCode.D);
		Bindings.fire = (KeyCode)PlayerPrefs.GetInt ("keyfire", (int)KeyCode.Mouse0);
		Bindings.classAbility = (KeyCode)PlayerPrefs.GetInt("keyclassability", (int)KeyCode.Space);
		Bindings.ability0 = (KeyCode)PlayerPrefs.GetInt("keyability0", (int)KeyCode.LeftShift);
		Bindings.ability1 = (KeyCode)PlayerPrefs.GetInt("keyability1", (int)KeyCode.E);
		Bindings.ability2 = (KeyCode)PlayerPrefs.GetInt("keyability2", (int)KeyCode.Q);
		Bindings.toggleInventory = (KeyCode)PlayerPrefs.GetInt("keyinventory", (int)KeyCode.Tab);
		Bindings.pause = (KeyCode)PlayerPrefs.GetInt("keypause", (int)KeyCode.Escape);

		// Options
		Options.tutorial = bool.Parse(PlayerPrefs.GetString("optiontutorial", "true"));
		Screen.fullScreen = bool.Parse(PlayerPrefs.GetString("optionfullscreen", "false"));
		mainAudio.SetFloat ("mastervolume", PlayerPrefs.GetFloat ("mastervolume", 0f));
		mainAudio.SetFloat ("musicvolume", PlayerPrefs.GetFloat ("musicvolume", 0f));
		mainAudio.SetFloat ("effectsvolume", PlayerPrefs.GetFloat ("effectsvolume", 0f));

		Debug.Log("Master init to: " + PlayerPrefs.GetFloat("mastervolume", -60f)); //DEBUG CODE
		Debug.Log("Music init to: " + PlayerPrefs.GetFloat("musicvolume", -60f)); //DEBUG CODE
		Debug.Log("Effects init to: " + PlayerPrefs.GetFloat("effectsvolume", -60f)); //DEBUG CODE
	}

	// Save all of the global options values to PlayerPrefs
	public void saveOptions()
	{
		// Bindings
		PlayerPrefs.SetInt("keyforward", (int)Bindings.forward);
		PlayerPrefs.SetInt("keystrafel", (int)Bindings.strafeL);
		PlayerPrefs.SetInt("keyreverse", (int)Bindings.reverse);
		PlayerPrefs.SetInt("keystrafer", (int)Bindings.strafeR);
		PlayerPrefs.SetInt("keyfire", (int)Bindings.fire);
		PlayerPrefs.SetInt("keyclassability", (int)Bindings.classAbility);
		PlayerPrefs.SetInt("keyability0", (int)Bindings.ability0);
		PlayerPrefs.SetInt("keyability1", (int)Bindings.ability1);
		PlayerPrefs.SetInt("keyability2", (int)Bindings.ability2);
		PlayerPrefs.SetInt("keypause", (int)Bindings.pause);
		PlayerPrefs.SetInt("keyinventory", (int)Bindings.toggleInventory);

		// Options
		PlayerPrefs.SetString("optiontutorial", Options.tutorial.ToString());
		PlayerPrefs.SetString ("optionfullscreen", Screen.fullScreen.ToString ());
		float masvol;
		mainAudio.GetFloat ("mastervolume", out masvol);
		PlayerPrefs.SetFloat ("mastervolume", masvol);
		float musvol;
		mainAudio.GetFloat ("musicvolume", out musvol);
		PlayerPrefs.SetFloat ("musicvolume", musvol);
		float effvol;
		mainAudio.GetFloat ("effectsvolume", out effvol);
		PlayerPrefs.SetFloat ("effectsvolume", effvol);

		Debug.Log ("Saving volume levels as: " + masvol + " " + musvol + " " + effvol);

		PlayerPrefs.Save ();
	}

	public void resetControlsToDefaults()
	{
		PlayerPrefs.DeleteKey ("keyforward");
		Bindings.forward = KeyCode.W;
		PlayerPrefs.DeleteKey ("keystrafel");
		Bindings.strafeL = KeyCode.A;
		PlayerPrefs.DeleteKey ("keyreverse");
		Bindings.reverse = KeyCode.S;
		PlayerPrefs.DeleteKey ("keystrafer");
		Bindings.strafeR = KeyCode.D;
		PlayerPrefs.DeleteKey ("keyfire");
		Bindings.fire = KeyCode.Mouse0;
		PlayerPrefs.DeleteKey ("keyclassability");
		Bindings.classAbility = KeyCode.Space;
		PlayerPrefs.DeleteKey ("keyability0");
		Bindings.ability0 = KeyCode.LeftShift;
		PlayerPrefs.DeleteKey ("keyability1");
		Bindings.ability1 = KeyCode.E;
		PlayerPrefs.DeleteKey ("keyability2");
		Bindings.ability2 = KeyCode.Q;
		PlayerPrefs.DeleteKey ("keyinventory");
		Bindings.toggleInventory = KeyCode.Tab;
		PlayerPrefs.DeleteKey ("keypause");
		Bindings.pause = KeyCode.Escape;
	}
}

// A global variable class that holds all key bindings
public static class Bindings
{
	public static KeyCode 
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
	pause;
}
	
// A global variable class that holds unique, non-default options
public static class Options
{
	//difficulty will only be loaded/saved on a per save-file basis
	public static Difficulty difficulty;

	public static bool tutorial;
}

public enum Difficulty{ Easy, Normal, Hard }