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
	protected float rotation;

	//the scale of the shape
	protected float scale;

	//use points withing the defined shape?
	protected bool filled;

	/* Constructor */
	public MinionFormation()
	{
		center = Vector2.zero;
		rotation = 0f;
		filled = false;
	}

	/* Accessors */
	public Vector2 Center
	{
		get{ return center; }
	}
	public float Rotation
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

	public abstract Vector2[] distribute (int n);
	public abstract void recenter (Vector2 center);
	public abstract void rotate (float rotation);
	public abstract void rescale (float scale);
}
