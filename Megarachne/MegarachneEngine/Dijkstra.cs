
using System;
using System.Collections.Generic;
using System.Linq;
using MegarachneEngine.Interfaces;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class Dijkstra : ISearch
    {
        public Dijkstra(Graph graph)
        {
            Graph = graph;
        }

        public void SetStartingDijkstraElementsArray()
        {
            PreviousArray = new int[Graph.Vertices.Count];
            PreviousEdgeArray = new int[Graph.Vertices.Count];
            Weights = new double[Graph.Vertices.Count];
            Visited = new bool[Graph.Vertices.Count];
            VisitedVertices = new List<Point3d>();
        }

        public void Search(int startVertexIndex)
        {
            SetStartingDijkstraElementsArray();

            Weights[startVertexIndex] = 0;

            List<DijkstraVertex> dijkstraVertices = new List<DijkstraVertex> {new DijkstraVertex(startVertexIndex, 0)};

            double[] edgesWeights = Graph.GetEdgesWeights();

            while (dijkstraVertices.Count != 0)
            {
                dijkstraVertices = dijkstraVertices.OrderBy(x => x.Weight).ToList();

                int currentVertexIndex = dijkstraVertices[0].Index;
                dijkstraVertices.RemoveAt(0);

                for (int i = 0; i < Graph.AdjacencyList.Vertices[currentVertexIndex].Count; i++)
                {
                    int neighborIndex = Graph.AdjacencyList.Vertices[currentVertexIndex][i];

                    if (Visited[neighborIndex])
                    {
                        continue;
                    }

                    int edgeToNeighbor = Graph.AdjacencyList.Edges[currentVertexIndex][i];
                    double weightToNeighbor = edgesWeights[edgeToNeighbor];

                    if (Weights[neighborIndex] == 0 || Weights[neighborIndex] > Weights[currentVertexIndex] + weightToNeighbor)
                    {
                        Weights[neighborIndex] = Weights[currentVertexIndex] + weightToNeighbor;
                        PreviousArray[neighborIndex] = currentVertexIndex;

                        dijkstraVertices.Add(new DijkstraVertex(neighborIndex, Weights[neighborIndex]));
                    }
                }
                Visited[currentVertexIndex] = true;
                VisitedVertices.Add(Graph.Vertices[currentVertexIndex]);
            }
        }

        public Path GetShortestPath(int startVertexIndex, int endVertexIndex)
        {
            if (startVertexIndex == endVertexIndex)
            {
                throw new ArgumentException("Start Vertex should be different from End Vertex");
            }

            SetStartingDijkstraElementsArray();

            Weights[startVertexIndex] = 0;

            List<DijkstraVertex> dijkstraVertices = new List<DijkstraVertex> { new DijkstraVertex(startVertexIndex, 0) };

            double[] edgesWeights = Graph.GetEdgesWeights();

            bool keepSearching = true;

            while (keepSearching || dijkstraVertices.Count != 0)
            {
                dijkstraVertices = dijkstraVertices.OrderBy(x => x.Weight).ToList();

                int currentVertexIndex = dijkstraVertices[0].Index;
                dijkstraVertices.RemoveAt(0);

                for (int i = 0; i < Graph.AdjacencyList.Vertices[currentVertexIndex].Count; i++)
                {
                    int neighborIndex = Graph.AdjacencyList.Vertices[currentVertexIndex][i];

                    if (Visited[neighborIndex])
                    {
                        continue;
                    }

                    int edgeToNeighbor = Graph.AdjacencyList.Edges[currentVertexIndex][i];
                    double weightToNeighbor = edgesWeights[edgeToNeighbor];

                    if (Weights[neighborIndex] == 0 || Weights[neighborIndex] > Weights[currentVertexIndex] + weightToNeighbor)
                    {
                        Weights[neighborIndex] = Weights[currentVertexIndex] + weightToNeighbor;
                        PreviousArray[neighborIndex] = currentVertexIndex;
                        PreviousEdgeArray[neighborIndex] = edgeToNeighbor;

                        dijkstraVertices.Add(new DijkstraVertex(neighborIndex, Weights[neighborIndex]));
                    }
                }
                Visited[currentVertexIndex] = true;
                VisitedVertices.Add(Graph.Vertices[currentVertexIndex]);

                if (currentVertexIndex == endVertexIndex)
                {
                    keepSearching = false;
                }
            }

            Path shortestPath = new Path();

            int vertexIndex = endVertexIndex;
            int edgeIndex = 0;

            while (PreviousArray[vertexIndex] != startVertexIndex)
            {
                edgeIndex = PreviousEdgeArray[vertexIndex];

                shortestPath.Edges.Add(Graph.Edges[edgeIndex]);
                shortestPath.EdgesIndexes.Add(edgeIndex);

                shortestPath.Vertices.Add(Graph.Vertices[vertexIndex]);
                shortestPath.VerticesIndexes.Add(vertexIndex);

                vertexIndex = PreviousArray[vertexIndex];
            }

            shortestPath.Edges.Add(Graph.Edges[edgeIndex]);
            shortestPath.EdgesIndexes.Add(edgeIndex);

            shortestPath.Vertices.Add(Graph.Vertices[vertexIndex]);
            shortestPath.VerticesIndexes.Add(vertexIndex);

            shortestPath.Edges.Reverse();
            shortestPath.EdgesIndexes.Reverse();
            shortestPath.Vertices.Reverse();
            shortestPath.VerticesIndexes.Reverse();

            return shortestPath;
        }

        public List<Point3d> VisitedVertices { get; private set; }

        public int[] PreviousArray { get; private set; }

        public int[] PreviousEdgeArray { get; private set; }

        public double[] Weights { get; private set; }

        public bool[] Visited { get; private set; }

        public Graph Graph { get; }
    }
}
