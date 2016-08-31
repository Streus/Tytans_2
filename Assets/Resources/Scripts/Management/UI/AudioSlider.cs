using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour {

	public string channelName;
	public AudioMixer mix;
	private Slider slider;

	// Use this for initialization
	void Start () {
		slider = transform.GetComponent<Slider> ();
		float temp;
		mix.GetFloat (channelName, out temp);
		slider.value = temp;
	}

	public void updateLevel(){
		mix.SetFloat (channelName, slider.value);
	}
}
