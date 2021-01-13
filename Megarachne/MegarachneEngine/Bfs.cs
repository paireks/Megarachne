using System;
using System.Collections.Generic;
using System.Linq;
using MegarachneEngine.Interfaces;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class Bfs : ISearch
    {
        public Bfs(Graph graph)
        {
            Graph = graph;
        }

        private void DeclareArrays()
        {
            PreviousArray = new int[Graph.Vertices.Count];
            PreviousEdgeArray = new int[Graph.Vertices.Count];
            Visited = new bool[Graph.Vertices.Count];
            VisitedVertices = new List<Point3d>();
        }

        public void Search(int startVertex, bool keepSearchForNotConnected)
        {
            DeclareArrays();

            Queue<int> queue = new Queue<int>();

            if (keepSearchForNotConnected)
            {
                MainLoopForSeparatedConnectedGraph(queue, Visited, PreviousArray, startVertex);

                for (int i = 0; i < Graph.Vertices.Count; i++)
                {
                    if (Visited[i] == false)
                    {
                        PreviousArray[i] = i;
                        MainLoopForSeparatedConnectedGraph(queue, Visited, PreviousArray, i);
                    }
                }
            }
            else
            {
                MainLoopForSeparatedConnectedGraph(queue, Visited, PreviousArray, startVertex);
            }
        }

        private void MainLoopForSeparatedConnectedGraph(Queue<int> queue, bool[] visited, int[] previous, int startVertex)
        {
            queue.Enqueue(startVertex);
            visited[startVertex] = true;
            VisitedVertices.Add(Graph.Vertices[startVertex]);

            while (queue.Count != 0)
            {
                int vertex = queue.Dequeue();

                foreach (int neighbor in Graph.AdjacencyList.Vertices[vertex])
                {
                    if (visited[neighbor])
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    VisitedVertices.Add(Graph.Vertices[neighbor]);
                    previous[neighbor] = vertex;
                }
            }
        }

        public bool IsGraphConnected()
        {
            DeclareArrays();

            Queue<int> queue = new Queue<int>();
            int startVertex = 0;

            MainLoopForSeparatedConnectedGraph(queue, Visited, PreviousArray, startVertex);

            return !Visited.Contains(false);
        }

        public Path GetShortestPath(int startVertexIndex, int endVertexIndex)
        {
            if (startVertexIndex == endVertexIndex)
            {
                throw new ArgumentException("Start Vertex should be different from End Vertex");
            }

            DeclareArrays();

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(startVertexIndex);
            Visited[startVertexIndex] = true;
            VisitedVertices.Add(Graph.Vertices[startVertexIndex]);

            bool foundEnd = false;
            while (queue.Count != 0 && !foundEnd)
            {
                int vertex = queue.Dequeue();

                for (var i = 0; i < Graph.AdjacencyList.Vertices[vertex].Count; i++)
                {
                    int neighbor = Graph.AdjacencyList.Vertices[vertex][i];
                    int neighborEdge = Graph.AdjacencyList.Edges[vertex][i];

                    if (Visited[neighbor])
                    {
                        continue;
                    }

                    queue.Enqueue(neighbor);
                    Visited[neighbor] = true;
                    VisitedVertices.Add(Graph.Vertices[neighbor]);
                    PreviousArray[neighbor] = vertex;
                    PreviousEdgeArray[neighbor] = neighborEdge;
                    if (neighbor == endVertexIndex)
                    {
                        foundEnd = true;
                        break;
                    }
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

        public int[] PreviousEdgeArray { get; private set; }

        public int[] PreviousArray { get; private set; }

        public Graph Graph { get; }

        public bool[] Visited { get; private set; }

    }
}
