using Rhino.Geometry;
using System;

namespace MegarachneEngine
{
    public class GraphPart
    {
        public GraphPart(Curve curve, bool isDirected)
        {
            StartVertex = curve.PointAtStart;
            EndVertex = curve.PointAtEnd;
            Edge = curve;
            EdgeWeight = curve.PointAtStart.DistanceTo(curve.PointAtEnd);
            IsDirected = isDirected;
        }

        public GraphPart(Vector3d vector, Point3d point, bool isDirected)
        {
            StartVertex = point;
            EndVertex = point + vector;

            Line edgeLine = new Line(point, vector);
            Edge = new LineCurve(edgeLine);
            EdgeWeight = vector.Length;
            IsDirected = isDirected;
        }

        public GraphPart(Point3d pointA, Point3d pointB, bool isDirected)
        {
            StartVertex = pointA;
            EndVertex = pointB;

            Line edgeLine = new Line(pointA, pointB);
            Edge = new LineCurve(edgeLine);
            EdgeWeight = pointA.DistanceTo(pointB);
            IsDirected = isDirected;
        }

        public override string ToString()
        {
            return string.Format("Graph Part{0}" +
                                 "Is Directed: {1}{0}" +
                                 "Edge Weight: {2}", Environment.NewLine, IsDirected, EdgeWeight);
        }

        public bool IsDirected { get; set; }
        public Point3d StartVertex { get; set; }
        public Point3d EndVertex { get; set; }
        public Curve Edge { get; set; }
        public double EdgeWeight { get; }

    }
}
