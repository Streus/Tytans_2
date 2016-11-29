using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionBox : MonoBehaviour
{
	private RectTransform rect;

	// Use this for initialization
	public void Awake ()
	{
		rect = transform.GetComponent<RectTransform> ();
	}

	public void Start()
	{
		adjustPosition ();
	}

	private void adjustPosition()
	{
		//TODO screen locking of pop-up boxes doesn't work yet

		//clamp anchorMin
		Vector3 botLeft = Camera.main.WorldToScreenPoint (rect.anchorMin);
		Mathf.Clamp01 (botLeft.x);
		Mathf.Clamp01 (botLeft.y);
		rect.anchorMin = Camera.main.ScreenToWorldPoint (botLeft);
		Debug.Log (rect.anchorMin.ToString ());

		//clamp anchorMax
		Vector3 topRight = Camera.main.WorldToScreenPoint (rect.anchorMax);
		Mathf.Clamp01 (topRight.x);
		Mathf.Clamp01 (topRight.y);
		rect.anchorMax = Camera.main.ScreenToWorldPoint (topRight);
		Debug.Log (rect.anchorMax.ToString ());
	}
}
