using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public static class Algorithm
    {
        public static int[] Bfs(Graph graph)
        {
            List<int>[] adjacencyList = graph.AdjacencyList;
            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int startVertex = 0;
            int[] previous = new int[graph.Vertices.Count];

            queue.Enqueue(startVertex);
            visited[startVertex] = true;
            
            while (queue.Count != 0)
            {
                int vertex = queue.Dequeue();

                foreach (int neighbor in adjacencyList[vertex])
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
            return previous;
        }

        public static bool BfsIsGraphConnected(Graph graph)
        {
            List<int>[] adjacencyList = graph.AdjacencyList;
            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int startVertex = 0;

            queue.Enqueue(startVertex);
            visited[startVertex] = true;

            while (queue.Count != 0)
            {
                int vertex = queue.Dequeue();

                if (adjacencyList[vertex] == null)
                {
                    continue;
                }

                foreach (int neighbor in adjacencyList[vertex])
                {
                    if (visited[neighbor])
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                }
            }

            bool isGraphConnected;

            if (visited.Contains(false))
            {
                isGraphConnected = false;
            }
            else
            {
                isGraphConnected = true;
            }

            return isGraphConnected;
        }
        public static List<int> BfsShortestPath(Graph graph, int startVertexIndex, int endVertexIndex)
        {
            List<int>[] adjacencyList = graph.AdjacencyList;
            bool[] visited = new bool[graph.Vertices.Count];
            Queue<int> queue = new Queue<int>();
            int startVertex = startVertexIndex;
            int[] previous = new int[graph.Vertices.Count];

            queue.Enqueue(startVertex);
            visited[startVertex] = true;

            bool keepSearching = true;
            while (queue.Count != 0 || keepSearching)
            {
                int vertex = queue.Dequeue();

                foreach (int neighbor in adjacencyList[vertex])
                {
                    if (visited[neighbor])
                    {
                        continue;
                    }
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    previous[neighbor] = vertex;
                    if (neighbor == endVertexIndex)
                    {
                        keepSearching = false;
                        break;
                    }
                }
            }

            List<int> shortestPath = new List<int>();

            shortestPath.Add(endVertexIndex);

            int currentVertex = endVertexIndex;

            while (previous[currentVertex] != startVertexIndex)
            {
                shortestPath.Add(previous[currentVertex]);
                currentVertex = previous[currentVertex];
            }

            shortestPath.Add(startVertexIndex);

            shortestPath.Reverse();

            return shortestPath;
        }

    }
}
