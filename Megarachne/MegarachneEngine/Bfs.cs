using System;
using System.Collections.Generic;
using System.Linq;

namespace MegarachneEngine
{
    public static class Bfs
    {
        public static int[] GetPrevious(Graph graph)
        {
            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int[] previous = new int[graph.Vertices.Count];

            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                if (visited[i] == false)
                {
                    previous[i] = i;
                    MainLoopForSeparatedConnectedGraph(queue, visited, previous, graph, i);
                }
            }

            return previous;
        }

        private static void MainLoopForSeparatedConnectedGraph(Queue<int> queue, bool[] visited, int[] previous, Graph graph, int startVertex)
        {
            queue.Enqueue(startVertex);
            visited[startVertex] = true;

            while (queue.Count != 0)
            {
                int vertex = queue.Dequeue();

                foreach (int neighbor in graph.AdjacencyList.Vertices[vertex])
                {
                    if (visited[neighbor])
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    previous[neighbor] = vertex;
                }
            }
        }

        public static bool IsGraphConnected(Graph graph)
        {
            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int[] previous = new int[graph.Vertices.Count];
            int startVertex = 0;

            MainLoopForSeparatedConnectedGraph(queue, visited, previous, graph, startVertex);

            return !visited.Contains(false);
        }

        public static List<int> GetShortestPath(Graph graph, int startVertex, int endVertex)
        {
            if (startVertex == endVertex)
            {
                throw new ArgumentException("Start Vertex should be different from End Vertex");
            }

            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int[] previous = new int[graph.Vertices.Count];

            queue.Enqueue(startVertex);
            visited[startVertex] = true;

            bool keepSearching = true;
            while (queue.Count != 0 || keepSearching)
            {
                int vertex = queue.Dequeue();

                foreach (int neighbor in graph.AdjacencyList.Vertices[vertex])
                {
                    if (visited[neighbor])
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    previous[neighbor] = vertex;
                    if (neighbor == endVertex)
                    {
                        keepSearching = false;
                        break;
                    }
                }
            }

            List<int> shortestPath = new List<int>();

            shortestPath.Add(endVertex);

            int currentVertex = endVertex;

            while (previous[currentVertex] != startVertex)
            {
                shortestPath.Add(previous[currentVertex]);
                currentVertex = previous[currentVertex];
            }

            shortestPath.Add(startVertex);

            shortestPath.Reverse();

            return shortestPath;
        }
    }
}
