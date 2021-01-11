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

        public void Search(int startVertex, bool keepSearchForNotConnected)
        {
            bool[] visited = new bool[Graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            PreviousArray = new int[Graph.Vertices.Count];
            VisitedVertices = new List<Point3d>();

            if (keepSearchForNotConnected)
            {
                MainLoopForSeparatedConnectedGraph(queue, visited, PreviousArray, startVertex);

                for (int i = 0; i < Graph.Vertices.Count; i++)
                {
                    if (visited[i] == false)
                    {
                        PreviousArray[i] = i;
                        MainLoopForSeparatedConnectedGraph(queue, visited, PreviousArray, i);
                    }
                }
            }
            else
            {
                MainLoopForSeparatedConnectedGraph(queue, visited, PreviousArray, startVertex);
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
            bool[] visited = new bool[Graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            PreviousArray = new int[Graph.Vertices.Count];
            int startVertex = 0;

            MainLoopForSeparatedConnectedGraph(queue, visited, PreviousArray, startVertex);

            return !visited.Contains(false);
        }

        public Path GetShortestPath(int startVertex, int endVertex)
        {
            if (startVertex == endVertex)
            {
                throw new ArgumentException("Start Vertex should be different from End Vertex");
            }

            bool[] visited = new bool[Graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            PreviousArray = new int[Graph.Vertices.Count];
            VisitedVertices = new List<Point3d>();
            int[] previousEdges = new int[Graph.Vertices.Count];

            queue.Enqueue(startVertex);
            visited[startVertex] = true;
            VisitedVertices.Add(Graph.Vertices[startVertex]);

            bool keepSearching = true;
            while (queue.Count != 0 && keepSearching)
            {
                int vertex = queue.Dequeue();

                for (var i = 0; i < Graph.AdjacencyList.Vertices[vertex].Count; i++)
                {
                    int neighbor = Graph.AdjacencyList.Vertices[vertex][i];
                    int neighborEdge = Graph.AdjacencyList.Edges[vertex][i];

                    if (visited[neighbor])
                    {
                        continue;
                    }

                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    VisitedVertices.Add(Graph.Vertices[neighbor]);
                    PreviousArray[neighbor] = vertex;
                    previousEdges[neighbor] = neighborEdge; 
                    if (neighbor == endVertex)
                    {
                        keepSearching = false;
                        break;
                    }
                }
            }

            Path shortestPath = new Path();

            int currentVertex = endVertex;
            int edgeIndex = 0;

            while (PreviousArray[currentVertex] != startVertex)
            {
                edgeIndex = previousEdges[currentVertex];
                shortestPath.Edges.Add(Graph.Edges[edgeIndex]);
                shortestPath.EdgesIndexes.Add(edgeIndex);

                shortestPath.Vertices.Add(Graph.Vertices[currentVertex]);
                shortestPath.VerticesIndexes.Add(currentVertex);

                currentVertex = PreviousArray[currentVertex];
            }

            shortestPath.Edges.Add(Graph.Edges[edgeIndex]);
            shortestPath.EdgesIndexes.Add(edgeIndex);

            shortestPath.Vertices.Add(Graph.Vertices[currentVertex]);
            shortestPath.VerticesIndexes.Add(currentVertex);

            shortestPath.Edges.Reverse();
            shortestPath.EdgesIndexes.Reverse();
            shortestPath.Vertices.Reverse();
            shortestPath.VerticesIndexes.Reverse();

            return shortestPath;
        }

        public List<Point3d> VisitedVertices { get; private set; }

        public int[] PreviousArray { get; private set; }

        public Graph Graph { get; }

    }
}
