using UnityEngine;
using System.Collections;

/* Author: Sam "Streus" Streed
 * Date: 11/10/2016
 */
public class PolyFormation : MinionFormation
{
	/* Static templates */

	public const Vector2[] POINT = new Vector2[]{ Vector2.zero };
	public const Vector2[] SQUARE = new Vector2[]{ new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1) };

	/* Instance vars */

	// The points of each vertex in the polygon
	private Vector2[] points;

	// Connect the first and last vertexes if true
	// Leave them unconnected if false
	private bool isPolygon;

	/* Constructors */

	// Default
	public PolyFormation() : base()
	{
		points = POINT;
		isPolygon = false;
	}

	// Take a list of free points w/ default base parameters
	public PolyFormation(bool isPolygon, params Vector2[] points) : base()
	{
		this.isPolygon = isPolygon;
		this.points = points;
	}

	// Take a pre-made list of points w/ default base parameters
	public PolyFormation(bool isPolygon, Vector2[] points) : base()
	{
		this.isPolygon = isPolygon;
		this.points = points;
	}

	// Full constructor
	public PolyFormation(bool isPolygon, Vector2 center, Quaternion rotation, bool filled, Vector2[] points)
	{
		this.isPolygon = isPolygon;
		this.points = points;
		recenter (center);
		rotate (rotation);
		this.filled = filled;
	}

	/* Accessors */
	public Vector2[] Points
	{
		get{ return points; }
	}
	public bool IsPolygon
	{
		get{ return isPolygon; }
		set{ isPolygon = value; }
	}

	/* Body Methods */

	// Set a new center for this polygon and update all points accordingly
	public override void recenter (Vector2 center)
	{
		this.center = center;
		for (int i = 0; i < points.Length; i++)
		{
			points [i] += center;
		}
	}

	// Set the rotation of this polygon around its center
	public override void rotate (Quaternion rotation)
	{

	}

	// Set the scale of this shape
	public override void scale (float scale)
	{
		float scaleFactor = scale / this.scale;
		this.scale = scale;
		for (int i = 0; i < points.Length; i++)
		{
			points [i] *= scaleFactor;
		}
	}

	// Create a distribution n objects equally spaced in this polygon
	public override Vector2[] distribute (int n)
	{
		throw new System.NotImplementedException ();
	}
}
