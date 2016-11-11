using UnityEngine;
using System.Collections;

/* Author: Sam "Streus" Streed
 * Date: 11/10/2016
 */
public class EclipseFormation : MinionFormation 
{
	/* Instance vars */

	//the focal points of the eclipse
	private Vector2 focalPoint1;
	private Vector2 focalPoint2;

	//the radius of the eclipse
	private float radius;

	/* Constructors */

	// Default
	public EclipseFormation() : base()
	{
		focalPoint1 = focalPoint2 = Vector2.zero;
		radius = 1f;
	}

	/* Body Methods */

	public override void recenter (Vector2 center)
	{
		this.center = center;
	}

	public override void rescale (float scale)
	{
		throw new System.NotImplementedException ();
	}

	public override void rotate (float rotation)
	{
		throw new System.NotImplementedException ();
	}

	public override Vector2[] distribute (int n)
	{
		if (focalPoint1 == focalPoint2)
			return circleDistribute (n);
		else
			return elipseDistribute (n);
	}

	private Vector2[] circleDistribute(int n)
	{
		throw new System.NotImplementedException ();
	}

	private Vector2[] elipseDistribute(int n)
	{
		throw new System.NotImplementedException ();
	}
}
