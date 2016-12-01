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

		Canvas canvas = rect.root.GetComponent<Canvas> ();
		Rect canvasBounds = canvas.pixelRect;

		//upper right
		rect.offsetMax = new Vector2(Mathf.Min (rect.offsetMax.x, canvasBounds.max.x), Mathf.Min (rect.offsetMax.y, canvasBounds.max.y));

		//lower left
		rect.offsetMin = new Vector2(Mathf.Max (rect.offsetMin.x, canvasBounds.min.x), Mathf.Max(rect.offsetMin.y, canvasBounds.min.y));
	}
}
