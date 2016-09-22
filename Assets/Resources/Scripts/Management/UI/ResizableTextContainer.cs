using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResizableTextContainer : MonoBehaviour {

	private Text textComponent;
	private RectTransform textRect;
	private RectTransform containerRect;

	// Use this for initialization
	void Start () {
		textComponent = transform.GetChild(0).GetComponent<Text>();
		textRect = transform.GetChild(0).GetComponent<RectTransform>();
		containerRect = transform.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float newHeight = LayoutUtility.GetPreferredHeight(textRect);
		containerRect.offsetMax = new Vector2(containerRect.offsetMax.x, newHeight);
	}
}
