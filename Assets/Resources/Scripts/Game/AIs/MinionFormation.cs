using UnityEngine;
using System.Collections;

public class MinionFormation
{
	// Static templates
	public const MinionFormation POINT = 
		new MinionFormation();

	// Instance variables
	//for use with polygons/lines
	private Vector2[] points;

	//for use with elipses/circles
	private Vector2 focalPoint1;
	private Vector2 focalPoint2;
	private float radius;

	//use points withing the defined shape?
	private bool filled;

	//number of discrete positions to distribute throughout the shape
	private int numPositions;

	//determines the type of shape the formation will conform to
	private FormationShapeType shapeType;

	// Empty constructor
	//creates a formation that is a single point at Vector2.zero
	public MinionFormation()
	{
		points = new Vector2[]{ Vector2.zero };
		focalPoint1 = focalPoint2 = Vector2.zero;
		shapeType = FormationShapeType.LINE;
		numPositions = 1;
	}

	// Polygon/Line Constructor
	public MinionFormation(bool isPolygon, bool isFilled, int numPositions, params Vector2[] points)
	{
		if (isPolygon) {
			shapeType = FormationShapeType.POLYGON;
			filled = isFilled;
		} else {
			shapeType = FormationShapeType.LINE;
			filled = false;
		}
		this.numPositions = numPositions;
		this.points = points.Clone ();
	}

	// Circle/Elipse Constructor
	public MinionFormation(bool isFilled, int numPositions, Vector2 focalPoint1, Vector2 focalPoint2, float radius)
	{
		filled = isFilled;
		this.numPositions = numPositions;
		this.focalPoint1 = focalPoint1;
		this.focalPoint2 = focalPoint2;
		this.radius = radius;
	}

	// Shape type enum
	public enum FormationShapeType
	{
		POLYGON, LINE, ELIPSE
	}
}
