using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour {

	public AudioMixer mix;
	public string channelName;
	public Slider slider;

	// Use this for initialization
	void Awake () {
		slider = transform.GetComponent<Slider> ();
	}
	void Start () {
		float temp = 0f;
		mix.GetFloat (channelName, out temp);
		Debug.Log (channelName + " slider inits with: " + temp); //DEBUG CODE
		slider.value = temp;
	}

	public void updateLevel(){
		mix.SetFloat (channelName, slider.value);
	}
}
