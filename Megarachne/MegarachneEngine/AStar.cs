using System;
using System.Collections.Generic;
using System.Linq;
using MegarachneEngine.Interfaces;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class AStar : ISearch
    {
        public AStar(Graph graph)
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

        public Path GetShortestPath(int startVertexIndex, int endVertexIndex)
        {
            if (startVertexIndex == endVertexIndex)
            {
                throw new ArgumentException("Start Vertex should be different from End Vertex");
            }

            DeclareArrays();

            Weights[startVertexIndex] = 0;

            double absoluteDistanceToEnd = Graph.Vertices[startVertexIndex].DistanceTo(Graph.Vertices[endVertexIndex]);

            List<AStarVertex> aStarVertices = new List<AStarVertex> { new AStarVertex(startVertexIndex, 0, absoluteDistanceToEnd) };

            bool foundEnd = false;

            while (aStarVertices.Count != 0 && !foundEnd)
            {
                aStarVertices = aStarVertices.OrderBy(x => x.Weight + x.StraightLineDistanceToEnd).ToList();

                int currentVertexIndex = aStarVertices[0].Index;
                aStarVertices.RemoveAt(0);

                for (int i = 0; i < Graph.AdjacencyList.Vertices[currentVertexIndex].Count; i++)
                {
                    int neighborIndex = Graph.AdjacencyList.Vertices[currentVertexIndex][i];

                    if (Visited[neighborIndex])
                    {
                        continue;
                    }

                    int edgeToNeighbor = Graph.AdjacencyList.Edges[currentVertexIndex][i];
                    double weightToNeighbor = Graph.EdgesWeights[edgeToNeighbor];

                    absoluteDistanceToEnd = Graph.Vertices[neighborIndex].DistanceTo(Graph.Vertices[endVertexIndex]);

                    if (Weights[neighborIndex] > Weights[currentVertexIndex] + weightToNeighbor)
                    {
                        Weights[neighborIndex] = Weights[currentVertexIndex] + weightToNeighbor;
                        PreviousArray[neighborIndex] = currentVertexIndex;
                        PreviousEdgeArray[neighborIndex] = edgeToNeighbor;

                        AStarVertex newAStarVertex =
                            new AStarVertex(neighborIndex, Weights[neighborIndex], absoluteDistanceToEnd);
                        if (!aStarVertices.Contains(newAStarVertex))
                        {
                            aStarVertices.Add(newAStarVertex);
                        }
                        else
                        {
                            AStarVertex oldAStarVertex = aStarVertices.First(p => p.Index == neighborIndex);
                            oldAStarVertex.Weight = Weights[neighborIndex];
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
