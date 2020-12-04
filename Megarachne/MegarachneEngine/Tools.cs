using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public static class Tools
    {
        public static bool CheckIfPointsAreSame(Point3d pointA, Point3d pointB, double tolerance)
        {
            if (Math.Abs(pointA.X - pointB.X) > tolerance)
            {
                return false;
            }
            if (Math.Abs(pointA.Y - pointB.Y) > tolerance)
            {
                return false;
            }
            if (Math.Abs(pointA.Z - pointB.Z) > tolerance)
            {
                return false;
            }

            return true;
        }
    }
}
