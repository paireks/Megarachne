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

        public Graph(Mesh mesh)
        {
            Vertices = mesh.Vertices.ToPoint3dArray().ToList();

            int numberOfEdges = mesh.TopologyEdges.Count * 2;

            Edges = new Curve[numberOfEdges];
            GraphArray = new int[2, numberOfEdges];

            MeshFaceList meshFaces = mesh.Faces;

            List<int> analyzedEdges = new List<int>();

            int currentGraphArrayRow = 0;
            int currentEdge = 0;

            for (int i = 0; i < meshFaces.Count; i++)
            {
                MeshFace currentMeshFace = meshFaces[i];
                int[] currentEdgesOfFaceIndexes = mesh.TopologyEdges.GetEdgesForFace(i);

                int facesEdgeCounter = 0;

                foreach (int index in currentEdgesOfFaceIndexes)
                {
                    bool wasThatEdgeBefore = analyzedEdges.Contains(index);

                    if (wasThatEdgeBefore)
                    {
                        facesEdgeCounter += 1;
                        continue;
                    }
                    analyzedEdges.Add(index);

                    Curve newEdge = mesh.TopologyEdges.EdgeLine(index).ToNurbsCurve();
                    Edges[currentEdge] = newEdge;
                    currentEdge += 1;

                    Curve copyEdge = newEdge.DuplicateCurve();
                    copyEdge.Reverse();
                    Edges[currentEdge] = copyEdge;
                    currentEdge += 1;

                    switch (facesEdgeCounter)
                    {
                        case 0:
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.A;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.B;
                            currentGraphArrayRow += 1;
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.B;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.A;
                            currentGraphArrayRow += 1;
                            break;
                        case 1:
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.B;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.C;
                            currentGraphArrayRow += 1;
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.C;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.B;
                            currentGraphArrayRow += 1;
                            break;
                        case 2:
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.C;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.D;
                            currentGraphArrayRow += 1;
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.D;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.C;
                            currentGraphArrayRow += 1;
                            break;
                        case 3:
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.D;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.A;
                            currentGraphArrayRow += 1;
                            GraphArray[0, currentGraphArrayRow] = currentMeshFace.A;
                            GraphArray[1, currentGraphArrayRow] = currentMeshFace.D;
                            currentGraphArrayRow += 1;
                            break;
                    }
                    facesEdgeCounter += 1;
                }

            }
        }

        public int[,] GraphArray { get; set; }
        public Curve[] Edges { get; set; }
        public List<Point3d> Vertices { get; set; }

    }
}
