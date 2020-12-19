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

            foreach (var graphPart in graphParts)
            {
                bool foundDuplicateStartVertex = false;
                bool foundDuplicateEndVertex = false;

                for (int j = 0; j < Vertices.Count; j++)
                {
                    if (foundDuplicateStartVertex && foundDuplicateEndVertex)
                    {
                        break;
                    }
                    if (!foundDuplicateStartVertex && Tools.CheckIfPointsAreSame(graphPart.StartVertex, Vertices[j], tolerance))
                    {
                        GraphArray[0, edgesCount] = j;
                        foundDuplicateStartVertex = true;
                    }
                    if (!foundDuplicateEndVertex && Tools.CheckIfPointsAreSame(graphPart.EndVertex, Vertices[j], tolerance))
                    {
                        GraphArray[1, edgesCount] = j;
                        foundDuplicateEndVertex = true;
                    }
                }

                if (!foundDuplicateStartVertex)
                {
                    Vertices.Add(graphPart.StartVertex);
                    GraphArray[0, edgesCount] = Vertices.Count - 1;
                }
                if (!foundDuplicateEndVertex)
                {
                    Vertices.Add(graphPart.EndVertex);
                    GraphArray[1, edgesCount] = Vertices.Count - 1;
                }

                Edges[edgesCount] = graphPart.Edge;
                edgesCount += 1;
                if (!graphPart.IsDirected)
                {
                    Edges[edgesCount] = graphPart.Edge;
                    GraphArray[0, edgesCount] = GraphArray[1, edgesCount - 1];
                    GraphArray[1, edgesCount] = GraphArray[0, edgesCount - 1];

                    edgesCount += 1;
                }
            }
        }

        public int[,] GraphArray { get; set; }
        public Curve[] Edges { get; set; }
        public List<Point3d> Vertices { get; set; }

    }
}
