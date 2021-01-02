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

                int firstVertexIndex;
                int secondVertexIndex;

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

                if (!graphPart.IsDirected)
                {
                    Curve reversedEdge = graphPart.Edge.DuplicateCurve();
                    reversedEdge.Reverse();
                    Edges[edgesCount] = reversedEdge;
                    GraphArray[0, edgesCount] = GraphArray[1, edgesCount - 1];
                    GraphArray[1, edgesCount] = GraphArray[0, edgesCount - 1];

                    edgesCount += 1;
                }
            }

            AdjacencyList = new AdjacencyList(Vertices.Count, GraphArray);
        }

        public Graph(Mesh mesh)
        {
            Vertices = mesh.Vertices.ToPoint3dArray().ToList();

            int numberOfEdges = mesh.TopologyEdges.Count * 2;

            Edges = new Curve[numberOfEdges];
            GraphArray = new int[2, numberOfEdges];
            AdjacencyList = new AdjacencyList(Vertices.Count);

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

                AdjacencyList.Vertices[firstVertexIndex].Add(secondVertexIndex);
                AdjacencyList.Edges[firstVertexIndex].Add(currentEdgeCount);
                currentEdgeCount += 1;

                Curve duplicateReversedEdge = currentEdge.DuplicateCurve();
                duplicateReversedEdge.Reverse();
                Edges[currentEdgeCount] = duplicateReversedEdge;
                GraphArray[0, currentEdgeCount] = secondVertexIndex;
                GraphArray[1, currentEdgeCount] = firstVertexIndex;

                AdjacencyList.Vertices[secondVertexIndex].Add(firstVertexIndex);
                AdjacencyList.Edges[secondVertexIndex].Add(currentEdgeCount);
                currentEdgeCount += 1;
            }
        }

        public override string ToString()
        {
            return string.Format("Graph{0}" +
                                 "Vertices: {1}{0}" +
                                 "Edges: {2}", Environment.NewLine, Vertices.Count, Edges.Length);
        }

        public int GetClosestVertexIndex(Point3d point)
        {
            PointCloud pointCloud = new PointCloud(Vertices);

            return pointCloud.ClosestPoint(point);
        }

        public int GetVertexOutdegree(int vertexIndex)
        {
            return AdjacencyList.Vertices[vertexIndex].Count;
        }

        public int GetVertexIndegree(int vertexIndex)
        {
            int inDegree = 0;
            foreach (var neighborsList in AdjacencyList.Vertices)
            {
                foreach (var neighbor in neighborsList)
                {
                    if (neighbor == vertexIndex)
                    {
                        inDegree++;
                    }
                }
            }
            return inDegree;
        }

        public int GetVertexDegree(int vertexIndex)
        {
            return GetVertexOutdegree(vertexIndex) + GetVertexIndegree(vertexIndex);
        }

        public int GetGraphDegree()
        {
            int max = 0;
            for (int i = 0; i < Vertices.Count; i++)
            {
                int currentVertexDegree = GetVertexDegree(i);
                if (currentVertexDegree > max)
                {
                    max = currentVertexDegree;
                }
            }

            return max;
        }

        public int[] GetVertexIndicesArray()
        {
            int[] arrayOfVertexIndices = new int[Vertices.Count];

            for (int i = 0; i < Vertices.Count; i++)
            {
                arrayOfVertexIndices[i] = i;
            }

            return arrayOfVertexIndices;
        }

        public double[] GetEdgeWeights()
        {
            double[] weightsOfEdges = new double[Edges.Length];

            for (int i = 0; i < Edges.Length; i++)
            {
                weightsOfEdges[i] = Edges[i].GetLength();
            }

            return weightsOfEdges;
        }

        public int[,] GraphArray { get; }
        public AdjacencyList AdjacencyList { get; }
        public Curve[] Edges { get; }
        public List<Point3d> Vertices { get; }

    }
}
