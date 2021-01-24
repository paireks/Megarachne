using System;
using System.Collections.Generic;
using System.Text;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public static class Tools
    {
        public static List<string> ShowGraphArrayStringRepresentation(int[,] graphArray)
        {
            List<string> graphArrayStringRepresentation = new List<string>();

            for (int i = 0; i < graphArray.GetLength(1); i++)
            {
                graphArrayStringRepresentation.Add(graphArray[0,i] + ";" + graphArray[1,i]);
            }

            return graphArrayStringRepresentation;
        }

        public static string GraphArrayToReportPart(int[,] graphArray, double[] edgeWeights, bool direction, bool showWeights, string textFormat)
        {
            StringBuilder reportPart = new StringBuilder();

            reportPart.AppendFormat("```mermaid" + Environment.NewLine);
            reportPart.AppendFormat(direction ? "graph LR" : "graph TD");
            reportPart.AppendFormat(Environment.NewLine);

            int arrayLength = graphArray.GetLength(1);

            if (arrayLength > 100)
            {
                throw new ArgumentException(
                    "Graph is too large to create decent flowchart with it. The limit is 100 edges");
            }

            if (showWeights)
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    reportPart.AppendFormat(graphArray[0, i] + "--" + string.Format(textFormat, edgeWeights[i]) + "-->" + graphArray[1, i] + ";" + Environment.NewLine);
                }
            }
            else
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    reportPart.AppendFormat(graphArray[0, i] + "-->" + graphArray[1, i] + ";" + Environment.NewLine);
                }
            }


            reportPart.AppendFormat("```");

            return reportPart.ToString();
        }

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
