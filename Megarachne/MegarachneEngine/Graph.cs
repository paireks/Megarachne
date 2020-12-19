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

            int numberOfEdges = 0;
            foreach (var graphPart in graphParts)
            {
                if (graphPart.IsDirected)
                {
                    numberOfEdges += 1;
                }
                else
                {
                    numberOfEdges += 2;
                }
            }

            Edges = new Curve[numberOfEdges];
            GraphArray = new int[2, numberOfEdges];

            int edgesCount = 0;

            for (int i = 0; i < graphParts.Count; i++)
            {
                bool foundDuplicateStartVertex = false;
                bool foundDuplicateEndVertex = false;

                for (int j = 0; j < Vertices.Count; j++)
                {
                    if (foundDuplicateStartVertex && foundDuplicateEndVertex)
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

                Edges[i] = graphParts[i].Edge;
            }
        }

        public int[,] GraphArray { get; set; }
        public Curve[] Edges { get; set; }
        public List<Point3d> Vertices { get; set; }

    }
}
