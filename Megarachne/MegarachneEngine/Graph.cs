using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MegarachneEngine
{
    public class Graph
    {
        public Graph(List<GraphPart> graphParts, double tolerance)
        {
            Vertices = new List<Point3d>();
            Edges = new Curve[graphParts.Count];
            GraphArray = new int[graphParts.Count, 2];

            for (int i = 0; i < graphParts.Count; i++)
            {
                Edges[i] = graphParts[i].Edge;

                bool foundDuplicateStartVertex = false;
                bool foundDuplicateEndVertex = false;

                for (int j = 0; j < Vertices.Count; j++)
                {
                    if (!foundDuplicateStartVertex && !foundDuplicateEndVertex)
                    {
                        break;
                    }
                    if (!foundDuplicateStartVertex && Tools.CheckIfPointsAreSame(graphParts[i].StartVertex, Vertices[j], tolerance))
                    {
                        GraphArray[0, i] = j;
                        foundDuplicateStartVertex = true;
                    }
                    if (!foundDuplicateEndVertex && Tools.CheckIfPointsAreSame(graphParts[i].EndVertex, Vertices[j], tolerance))
                    {
                        GraphArray[1, i] = j;
                        foundDuplicateEndVertex = true;
                    }
                }

                if (!foundDuplicateStartVertex)
                {
                    Vertices.Add(graphParts[i].StartVertex);
                    GraphArray[0, i] = Vertices.Count - 1;
                }
                if (!foundDuplicateEndVertex)
                {
                    Vertices.Add(graphParts[i].EndVertex);
                    GraphArray[1, i] = Vertices.Count - 1;
                }
            }
        }

        public int[,] GraphArray { get; set; }
        public Curve[] Edges { get; set; }
        public List<Point3d> Vertices { get; set; }
    }
}
