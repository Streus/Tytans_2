using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitText : MonoBehaviour {

	public float duration;
	public Text textComp;
	public GameObject canvas;
	private Vector2 offset;
	private float dX;
	private Vector2 origin;

	// Use this for initialization
	void Awake () {
		textComp = transform.GetComponent<Text>();
		canvas = GameObject.Find ("GUI");
		transform.SetParent (canvas.transform, false);

		dX = (Random.value * 2f) - 1f;
	}
	
	// Update is called once per frame
	void Update () {
		offset = new Vector2(offset.x + (dX * Time.deltaTime), offset.y + 1f * Time.deltaTime);
		textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, textComp.color.a - Time.deltaTime/duration);

		duration -= Time.deltaTime;
		if(duration <= 0f)
			Destroy(gameObject);
	}

	void LateUpdate () {
		transform.GetComponent<RectTransform> ().anchoredPosition = Camera.main.WorldToScreenPoint (origin + offset);
	}

	public void setParentPosition(Vector2 position)
	{
		origin = position;
		offset = Vector2.zero;
	}
}
