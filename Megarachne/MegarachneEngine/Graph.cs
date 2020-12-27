using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry.Collections;

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

                int firstVertexIndex = 0;
                int secondVertexIndex = 0;

                for (int j = 0; j < Vertices.Count; j++)
                {
                    if (foundDuplicateStartVertex && foundDuplicateEndVertex)
                    {
                        break;
                    }
                    if (!foundDuplicateStartVertex && Tools.CheckIfPointsAreSame(graphPart.StartVertex, Vertices[j], tolerance))
                    {
                        firstVertexIndex = j;
                        GraphArray[0, edgesCount] = firstVertexIndex;
                        foundDuplicateStartVertex = true;
                    }
                    if (!foundDuplicateEndVertex && Tools.CheckIfPointsAreSame(graphPart.EndVertex, Vertices[j], tolerance))
                    {
                        secondVertexIndex = j;
                        GraphArray[1, edgesCount] = secondVertexIndex;
                        foundDuplicateEndVertex = true;
                    }
                }

                if (!foundDuplicateStartVertex)
                {
                    Vertices.Add(graphPart.StartVertex);
                    firstVertexIndex = Vertices.Count - 1;
                    GraphArray[0, edgesCount] = firstVertexIndex;
                }
                if (!foundDuplicateEndVertex)
                {
                    Vertices.Add(graphPart.EndVertex);
                    secondVertexIndex = Vertices.Count - 1;
                    GraphArray[1, edgesCount] = secondVertexIndex;
                }

                Edges[edgesCount] = graphPart.Edge;
                edgesCount += 1;

                if (AdjacencyList[firstVertexIndex] == null)
                {
                    AdjacencyList[firstVertexIndex] = new List<int>();
                }
                AdjacencyList[firstVertexIndex].Add(secondVertexIndex);

                if (!graphPart.IsDirected)
                {
                    Curve reversedEdge = graphPart.Edge.DuplicateCurve();
                    reversedEdge.Reverse();
                    Edges[edgesCount] = reversedEdge;
                    GraphArray[0, edgesCount] = GraphArray[1, edgesCount - 1];
                    GraphArray[1, edgesCount] = GraphArray[0, edgesCount - 1];

                    edgesCount += 1;

                    if (AdjacencyList[secondVertexIndex] == null)
                    {
                        AdjacencyList[secondVertexIndex] = new List<int>();
                    }
                    AdjacencyList[secondVertexIndex].Add(firstVertexIndex);
                }
            }
        }

        public Graph(Mesh mesh)
        {
            Vertices = mesh.Vertices.ToPoint3dArray().ToList();

            int numberOfEdges = mesh.TopologyEdges.Count * 2;

            Edges = new Curve[numberOfEdges];
            GraphArray = new int[2, numberOfEdges];
            AdjacencyList = new List<int>[Vertices.Count];

            MeshTopologyEdgeList topologyEdgeList = mesh.TopologyEdges;

            int currentEdgeCount = 0;

            for (int i = 0; i < topologyEdgeList.Count; i++)
            {
                int firstVertexIndex = topologyEdgeList.GetTopologyVertices(i).I;
                int secondVertexIndex = topologyEdgeList.GetTopologyVertices(i).J;

                Curve currentEdge = topologyEdgeList.EdgeLine(i).ToNurbsCurve();
                Edges[currentEdgeCount] = currentEdge;
                GraphArray[0, currentEdgeCount] = firstVertexIndex;
                GraphArray[1, currentEdgeCount] = secondVertexIndex;
                if (AdjacencyList[firstVertexIndex] == null)
                {
                    AdjacencyList[firstVertexIndex] = new List<int>();
                }
                AdjacencyList[firstVertexIndex].Add(secondVertexIndex);
                currentEdgeCount += 1;

                Curve duplicateReversedEdge = currentEdge.DuplicateCurve();
                duplicateReversedEdge.Reverse();
                Edges[currentEdgeCount] = duplicateReversedEdge;
                GraphArray[0, currentEdgeCount] = secondVertexIndex;
                GraphArray[1, currentEdgeCount] = firstVertexIndex;
                if (AdjacencyList[secondVertexIndex] == null)
                {
                    AdjacencyList[secondVertexIndex] = new List<int>();
                }
                AdjacencyList[secondVertexIndex].Add(firstVertexIndex);
                currentEdgeCount += 1;
            }
        }

        public int GetClosestVertexIndex(Point3d point)
        {
            PointCloud pointCloud = new PointCloud(Vertices);

            return pointCloud.ClosestPoint(point);
        }

        public int GetVertexDegree(int vertexIndex)
        {
            return AdjacencyList[vertexIndex].Count;
        }

        public int GetGraphDegree()
        {
            int max = 0;

            foreach (var listOfIndexes in AdjacencyList)
            {
                int currentMax = listOfIndexes.Count;
                if (max < currentMax)
                {
                    max = currentMax;
                }
            }

            return max;
        }

        public int[,] GraphArray { get; }
        public List<int>[] AdjacencyList { get; }
        public Curve[] Edges { get; }
        public List<Point3d> Vertices { get; }

    }
}
