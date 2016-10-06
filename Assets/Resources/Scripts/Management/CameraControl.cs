using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private GameObject player;
	private Vector3 offset;
	private Transform cam;

	//camera shake varibles
	private float intensity;
	private float shakeTime;

	// Use this for initialization
	void Start () {
		GameManager.cameraController = this;

		player = GameManager.player;
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
		offset = transform.position - player.transform.position;

		cam = transform.GetChild (0);
	}

	void Update () {
		if (shakeTime > 0f) {
			Vector3 shakePos = Random.insideUnitSphere * intensity;
			cam.transform.localPosition = shakePos;
			shakeTime -= Time.deltaTime;
		} else {
			cam.transform.localPosition = Vector3.zero;
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
