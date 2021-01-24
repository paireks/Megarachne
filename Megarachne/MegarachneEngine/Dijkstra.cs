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

        private void DeclareArrays()
        {
            PreviousArray = new int[Graph.Vertices.Count];
            PreviousEdgeArray = new int[Graph.Vertices.Count];
            Weights = new double[Graph.Vertices.Count];

            for (int i = 0; i < PreviousArray.Length; i++)
            {
                PreviousArray[i] = -1;
                PreviousEdgeArray[i] = -1;
                Weights[i] = double.PositiveInfinity;
            }

            Visited = new bool[Graph.Vertices.Count];
            VisitedVertices = new List<Point3d>();
        }

        public void Search(int startVertexIndex)
        {
            DeclareArrays();

            Weights[startVertexIndex] = 0;

            List<DijkstraVertex> dijkstraVertices = new List<DijkstraVertex> {new DijkstraVertex(startVertexIndex, 0)};

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
                    double weightToNeighbor = Graph.EdgesWeights[edgeToNeighbor];

                    if (Weights[neighborIndex] == 0 || Weights[neighborIndex] > Weights[currentVertexIndex] + weightToNeighbor)
                    {
                        Weights[neighborIndex] = Weights[currentVertexIndex] + weightToNeighbor;
                        PreviousArray[neighborIndex] = currentVertexIndex;

                        DijkstraVertex newDijkstraVertex = new DijkstraVertex(neighborIndex, Weights[neighborIndex]);
                        if (!dijkstraVertices.Contains(newDijkstraVertex))
                        {
                            dijkstraVertices.Add(newDijkstraVertex);
                        }
                        else
                        {
                            DijkstraVertex oldDijkstraVertex = dijkstraVertices.First(p => p.Index == neighborIndex);
                            oldDijkstraVertex.Weight = Weights[neighborIndex];
                        }
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

            DeclareArrays();

            Weights[startVertexIndex] = 0;

            List<DijkstraVertex> dijkstraVertices = new List<DijkstraVertex> { new DijkstraVertex(startVertexIndex, 0) };

            bool foundEnd = false;

            while (dijkstraVertices.Count != 0 && !foundEnd)
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
                    double weightToNeighbor = Graph.EdgesWeights[edgeToNeighbor];

                    if (Weights[neighborIndex] == 0 || Weights[neighborIndex] > Weights[currentVertexIndex] + weightToNeighbor)
                    {
                        Weights[neighborIndex] = Weights[currentVertexIndex] + weightToNeighbor;
                        PreviousArray[neighborIndex] = currentVertexIndex;
                        PreviousEdgeArray[neighborIndex] = edgeToNeighbor;

                        DijkstraVertex newDijkstraVertex = new DijkstraVertex(neighborIndex, Weights[neighborIndex]);
                        if (!dijkstraVertices.Contains(newDijkstraVertex))
                        {
                            dijkstraVertices.Add(newDijkstraVertex);
                        }
                        else
                        {
                            DijkstraVertex oldDijkstraVertex = dijkstraVertices.First(p => p.Index == neighborIndex);
                            oldDijkstraVertex.Weight = Weights[neighborIndex];
                        }
                    }
                }
                Visited[currentVertexIndex] = true;
                VisitedVertices.Add(Graph.Vertices[currentVertexIndex]);

                if (currentVertexIndex == endVertexIndex)
                {
                    foundEnd = true;
                }
            }

            if (foundEnd)
            {
                Path shortestPath = new Path(startVertexIndex, endVertexIndex, Graph, PreviousArray, PreviousEdgeArray);
                return shortestPath;
            }

            throw new ArgumentException("Couldn't find a correct path between those vertices");
        }

        public List<Point3d> VisitedVertices { get; private set; }

        public int[] PreviousArray { get; private set; }

        public int[] PreviousEdgeArray { get; private set; }

        public double[] Weights { get; private set; }

        public bool[] Visited { get; private set; }

        public Graph Graph { get; }
    }
}
