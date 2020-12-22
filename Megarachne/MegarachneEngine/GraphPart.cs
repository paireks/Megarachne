using System.Collections.Generic;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class GraphPart
    {
        public GraphPart(Curve curve, bool isDirected)
        {
            StartVertex = curve.PointAtStart;
            EndVertex = curve.PointAtEnd;
            Edge = curve;
            IsDirected = isDirected;
        }

        public GraphPart(Vector3d vector, Point3d point, bool isDirected)
        {
            StartVertex = point;
            EndVertex = point + vector;

            Line edgeLine = new Line(point, vector);
            Edge = new LineCurve(edgeLine);

            IsDirected = isDirected;
        }

        public GraphPart(Point3d pointA, Point3d pointB, bool isDirected)
        {
            StartVertex = pointA;
            EndVertex = pointB;

            Line edgeLine = new Line(pointA, pointB);
            Edge = new LineCurve(edgeLine);

            IsDirected = isDirected;
        }



        public bool IsDirected { get; set; }
        public Point3d StartVertex { get; set; }
        public Point3d EndVertex { get; set; }
        public Curve Edge { get; set; }

    }
}
