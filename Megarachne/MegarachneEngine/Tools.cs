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

        public static List<string> ShowGraphArrayStringRepresentation(int[,] graphArray)
        {
            List<string> graphArrayStringRepresentation = new List<string>();

            for (int i = 0; i < graphArray.GetLength(1); i++)
            {
                graphArrayStringRepresentation.Add(graphArray[0,i] + ";" + graphArray[1,i]);
            }

            return graphArrayStringRepresentation;
        }

        public static List<string> ShowAdjacencyListStringRepresentation(List<int>[] adjacencyList)
        {
            List<string> adjacencyListStringRepresentation = new List<string>();

            foreach (var currentVertexIndexes in adjacencyList)
            {
                string currentString = "";
                foreach (var index in currentVertexIndexes)
                {
                    currentString += index + ";";
                }
                adjacencyListStringRepresentation.Add(currentString);
            }

            return adjacencyListStringRepresentation;
        }
    }
}
