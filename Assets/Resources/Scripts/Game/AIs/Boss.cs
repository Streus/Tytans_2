using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	protected Entity self;
	protected GameObject target;

	public int bossIndex;

	// Set up control
	public virtual void Awake()
	{
		if (GameManager.manager.completedBosses [bossIndex])
			Destroy (gameObject);

		self = transform.GetComponent<Entity> ();
		target = GameManager.player;
	}

	// Add a boss health bar to the GUI
	public void OnEnable()
	{
		//TODO boss health bar
	}
	
	// AI behavior
	public virtual void FixedUpdate()
	{
		if (!self.physbody.simulated)
			return;
	}

	// Drop items and mark boss as completed
	public virtual void OnDestroy() 
	{
		//mark as completed
		GameManager.manager.completedBosses [bossIndex] = true;
	}

	// Target accessor
	public GameObject Target
	{
		get{ return target; }
		set{ target = value; }
	}

	// Rotate this boss to face their given target
	protected void faceTarget() 
	{
		Vector3 tarPos = target.transform.position;
		Quaternion rot = Quaternion.LookRotation(transform.position - new Vector3(tarPos.x, tarPos.y, -100f), Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
	}

	// Use the ability at [index] if the ability is ready and the passed conditions are true
	protected bool useAbility(int index, params bool[] conditions)
	{
		if (!self.abilities [index].ready ())
			return false;
		for (int i = 0; i < conditions.Length; i++)
		{
			if (conditions [i] == false)
				return false;
		}
		self.abilities [index].use ();
		return true;
	}

	// useAbility without the extra conditions
	protected bool useAbility(int index)
	{
		return useAbility(index, true);
	}

}
