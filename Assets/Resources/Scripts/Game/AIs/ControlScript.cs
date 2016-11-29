using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour
{
	protected Rigidbody2D physbody;
	protected Entity self;

	protected GameObject target;

	public virtual void Awake()
	{
		physbody = transform.GetComponent<Rigidbody2D> ();
		self = transform.GetComponent<Entity> ();
	}

	public virtual void FixedUpdate()
	{
		if (!physbody.simulated || self.stunned > 0)
			return;
	}

	// Rotate to face the target object + its velocity
	protected void faceTargetLeading(GameObject tar, float bulletSpeed)
	{
		Rigidbody2D tarBody = tar.GetComponent<Rigidbody2D> ();
		if (tarBody == null)
			throw new System.ArgumentException ("Tried to lead a velocity-less GameObject");
		float stepsToCollision = Vector2.Distance (transform.position, tar.transform.position) / bulletSpeed;
		facePoint ((Vector2)tar.transform.position + (tarBody.velocity * stepsToCollision));
	}

	// Rotate this transform to face their given target
	protected void faceTarget(GameObject tar)
	{
		if(target != null)
			facePoint(tar.transform.position);
	}

	// Rotate this transform to face a given point
	protected void facePoint(Vector2 point)
	{
		Quaternion rot = Quaternion.LookRotation(transform.position - new Vector3(point.x, point.y, -100f), Vector3.back);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
	}

	// Target accessor
	public GameObject Target
	{
		get{ return target; }
		set{ target = value; }
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
