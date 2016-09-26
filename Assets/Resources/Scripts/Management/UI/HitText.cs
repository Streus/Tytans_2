using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitText : MonoBehaviour {

	public float duration;
	public Text textComp;

	// Use this for initialization
	void Awake () {
		textComp = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(transform.position.x, transform.position.y + 0.01f);
		textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, textComp.color.a - 1/duration);

		duration -= Time.deltaTime;
		if(duration <= 0f)
			Destroy(gameObject);
	}
}
