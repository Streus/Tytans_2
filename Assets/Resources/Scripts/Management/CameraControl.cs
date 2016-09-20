using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private GameObject player;
	private Vector3 offset;
	private Transform camera;

	//camera shake varibles
	private float intensity;
	private float shakeTime;

	void Awake () {
		camera = transform.GetChild (0);
	}

	// Use this for initialization
	void Start () {
		player = GameManager.player;
		offset = transform.position - player.transform.position;
	}

	void Update () {
		if (shakeTime > 0f) {
			camera.localPosition = Random.insideUnitSphere * intensity;
			shakeTime -= Time.deltaTime;
		} else {
			camera.localPosition = Vector3.zero;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}

	public void shakeCamera(float intensity, float shakeTime)
	{
		this.intensity = intensity;
		this.shakeTime = shakeTime;
	}
}
