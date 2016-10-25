using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

	private GameObject door;
	public bool open;

	// Initialization
	void Start ()
	{
		door = transform.GetChild (0).gameObject;
		open = false;
	}

	//Door "animation"
	public void Update()
	{
		if (open) 
		{
			if (door.transform.localScale.y > 0f)
				door.transform.localScale = new Vector3 (door.transform.localScale.x, Mathf.Clamp01(door.transform.localScale.y - 0.1f), door.transform.localScale.z);
			else
				door.SetActive (false);
		} 
		else if (!open) 
		{
			if (door.transform.localScale.y < 1f)
				door.transform.localScale = new Vector3 (door.transform.localScale.x, Mathf.Clamp01(door.transform.localScale.y + 0.1f), door.transform.localScale.z);
			else
				door.SetActive (true);
		}
	}

	// Door opening and closing
	public void toggleDoor()
	{
		setDoor (!open);
	}
	public void setDoor(bool state)
	{
		open = state;
	}
}
