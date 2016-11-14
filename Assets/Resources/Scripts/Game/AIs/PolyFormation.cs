using UnityEngine;
using System.Collections;

/* Author: Sam "Streus" Streed
 * Date: 11/14/2016
 */
public class PolyFormation : MinionFormation
{
	/* Static templates */

	public static Vector2[] POINT = new Vector2[] { 
		Vector2.zero 
	};
	public static Vector2[] SQUARE = new Vector2[] { 
		new Vector2(-1, 1), 
		new Vector2(1, 1), 
		new Vector2(1, -1), 
		new Vector2(-1, -1) 
	};
	public static Vector2[] TRAPEZOID = new Vector2[] {
		new Vector2 (-1, 2),
		new Vector2 (1, 1),
		new Vector2 (1, -1),
		new Vector2 (-1, -2)
	};
	public static Vector2[] HEXAGON = new Vector2[] { //TODO remove once auto generation of regular polygons is supported
		new Vector2 (-2, 1),
		new Vector2 (0, 2),
		new Vector2 (2, 1),
		new Vector2 (2, -1),
		new Vector2 (0, -2),
		new Vector2 (-2, -1)
	};

	/* Static Methods */

	// Create a Polyformation object with points that represent a regular polygon with n sides
	public static PolyFormation generateRegularPolygon(int sides, float radius)
	{
		throw new System.NotImplementedException ();
	}

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

	// Take a pre-made list of points w/ default base parameters
	public PolyFormation(bool isPolygon, Vector2[] points) : base()
	{
		this.isPolygon = isPolygon;
		this.points = points;
	}

	// Full constructor
	public PolyFormation(bool isPolygon, Vector2 center, float rotation, float scale, bool filled, Vector2[] points)
	{
		this.isPolygon = isPolygon;
		this.points = (Vector2[])points.Clone();
		recenter (center);
		rotate (rotation);
		rescale (scale);
		this.filled = filled;
	}

	/* Accessors */
	public Vector2[] Points
	{
		get{ return points; }
		set {
			points = (Vector2[])value.Clone();
			scale = 1f;
			rotation = 0f;
		}
			
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
		for (int i = 0; i < points.Length; i++)
		{
			points [i] -= this.center;
			points [i] += center;
		}
		this.center = center;
	}

	// Set the rotation (in degrees) of this polygon around its center
	public override void rotate (float rotation)
	{
		//recenter to zero and save old center for later
		Vector2 oldCenter = this.center;
		recenter (Vector2.zero);

		//normalize rotation and determine sin coeffs
		float sin1Coeff = 1f;
		float sin2Coeff = 1f;
		rotation = rotation % 360;
		rotation *= Mathf.Deg2Rad;
		if (rotation < 0)
			sin2Coeff = -1f;
		else
			sin1Coeff = 1f;

		//calculate the new point n using a rotation matrix on points[i]
		for (int i = 0; i < points.Length; i++)
		{
			Vector2 n = Vector2.zero;
			float cosR = Mathf.Cos (rotation);
			float sinR = Mathf.Sin (rotation);
			n.x = (points [i].x * cosR) + (sin1Coeff * points [i].y * sinR);
			n.y = (points [i].x * sin2Coeff * sinR) + (points [i].y * cosR);
			points [i] = n;
		}

		//set the center back
		recenter(oldCenter);
	}

	// Set the scale of this shape
	public override void rescale (float scale)
	{
		float scaleFactor = scale / this.scale;
		this.scale = scale;
		for (int i = 0; i < points.Length; i++)
		{
			points [i] *= scaleFactor;
		}
	}

	// Create a distribution of n equally spaced objects on this object's array of points
	public override Vector2[] distribute (int n)
	{
		if (!isPolygon || (isPolygon && !filled))
			return lineDistribute (n);
		else
			return polyFillDistribute (n);
	}

	// Create a distribution over a sequence of line segments betweeen points
	private Vector2[] lineDistribute (int n)
	{
		//find the perimiter of the shape denoted by points
		float distance = 0f;
		for (int i = 0; i < points.Length - 1; i++)
		{
			distance += Vector2.Distance (points [i], points [i + 1]);
		}
		//add line connecting first and last points if this is a polygon
		if (isPolygon)
			distance += Vector2.Distance (points [points.Length - 1], points [0]);

		//distance between objects
		float stepDistance = distance / n;

		//last point in points crossed
		int currentIndex = 0;

		//current object position being calculated
		Vector2 currentPoint = points [currentIndex];

		//the array of positions to return
		Vector2[] positions = new Vector2[n];

		//the distance between n + 1 and the last filled position
		float dpn = Vector2.Distance(currentPoint, points[currentIndex + 1]);

		int count = 0;
		while (distance > 0)
		{ 
			float distRatio = 1f;
			if (dpn - stepDistance > 0) //won't reach the next point
			{
				distRatio = (stepDistance / dpn);
			} 
			else if (dpn - stepDistance <= 0) //will reach or pass the next point
			{
				//update currentPoint and calculate the distance to progress from currentPoint to nextPoint
				currentPoint = points [(++currentIndex) % points.Length];
				float leftoverDistance = stepDistance - dpn;
				dpn = Vector2.Distance(currentPoint, points[(currentIndex + 1) % points.Length]);

				distRatio = (leftoverDistance / dpn);
			}

			Vector2 newPos = Vector2.zero;
			newPos.x = currentPoint.x + distRatio * (points [(currentIndex + 1) % points.Length].x - currentPoint.x);
			newPos.y = currentPoint.y + distRatio * (points [(currentIndex + 1) % points.Length].y - currentPoint.y);

			positions [count++] = currentPoint = newPos; //TODO this breaks at max minion count?
			dpn -= stepDistance;
			distance -= stepDistance;
		}
		return positions;
	}

	// Create a distribution over an area contained by points
	private Vector2[] polyFillDistribute (int n)
	{
		throw new System.NotImplementedException ();
	}

	// Make a string representation of this PolyFormation object
	public override string ToString ()
	{
		string pointsString = "";
		for (int i = 0; i < points.Length; i++)
		{
			pointsString += points [i].ToString () + "\n";
		}
		return string.Format ("[PolyFormation: Points:\n{0}\nIsPolygon = {1}\nCenter = {2}\nRotation = {3}\nScale = {4}]", pointsString, IsPolygon, Center, Rotation, Scale);
	}
}
