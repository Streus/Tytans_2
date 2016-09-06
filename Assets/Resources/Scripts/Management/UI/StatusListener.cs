using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StatusListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public StatusEffect status;
	private Text cooldownField;
	private Image image;

	// Use this for initialization
	void Start () {
		cooldownField = transform.GetChild (0).GetComponent<Text> ();
		image = transform.GetComponent<Image> ();

		image.sprite = status.icon;
	}
	
	// Update is called once per frame
	void Update () {
		if (status.duration <= 0)
			Destroy (gameObject);
		cooldownField.text = status.duration.ToString ("###");
	}

	public void setStatus(StatusEffect status)
	{
		this.status = status;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log ("Add description box pop-up!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		
	}
}
