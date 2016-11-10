using UnityEngine;
using System.Collections;

/* Author: Sam "Streus" Streed
 * Date: 11/10/2016
 */
public abstract class MinionFormation
{
	/* Instance vars */

	//the center of the shape
	protected Vector2 center;

	//the rotation of the shape
	protected Quaternion rotation;

	//the scale of the shape
	protected float scale;

	//use points withing the defined shape?
	protected bool filled;

	/* Constructor */
	public MinionFormation()
	{
		center = Vector2.zero;
		rotation = Quaternion.identity;
		filled = false;
	}

	/* Accessors */
	public Vector2 Center
	{
		get{ return center; }
	}
	public Quaternion Rotation
	{
		get{ return rotation; }
	}
	public float Scale
	{
		get{ return scale; }
	}
	public bool Filled
	{
		get{ return filled; }
		set{ filled = value; }
	}

	public abstract Vector2[] distribute (int numPositions);
	public abstract void recenter (Vector2 center);
	public abstract void rotate (Quaternion rotation);
	public abstract void scale (float scale);
}
