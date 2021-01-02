using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public static class Dijkstra
    {
        private static PriorityQueue<DijkstraElement> SetStartingPriorityQueue(Graph graph, int startVertexIndex)
        {
            PriorityQueue<DijkstraElement> queue = new PriorityQueue<DijkstraElement>();

            for (int i = 0; i < startVertexIndex; i++)
            {
                queue.Enqueue(new DijkstraElement(i, double.MaxValue, -1, false));
            }

            queue.Enqueue(new DijkstraElement(startVertexIndex, 0, -1, false));

            for (int i = startVertexIndex + 1; i < graph.Vertices.Count; i++)
            {
                queue.Enqueue(new DijkstraElement(i, double.MaxValue, -1, false));
            }

            return queue;
        } 

        /*public static Path GetShortestPath(Graph graph, int startVertexIndex, int endVertexIndex)
        {
            PriorityQueue<DijkstraElement> queue = SetStartingPriorityQueue(graph, startVertexIndex);



            /*int amountOfVertices = graph.Vertices.Count;

            double[] d = new double[amountOfVertices];
            int[] p = new int[amountOfVertices];
            int[] q = graph.GetVertexIndexesArray();
            //int[] s = new int[amountOfVertices];

            double[] edgesWeights = graph.GetEdgesWeights();

            for (int i = 0; i < amountOfVertices; i++)
            {
                d[i] = double.MaxValue;
                p[i] = -1;
            }

            d[startVertex] = 0;

            foreach (int vertex in q)
            {
                for (var index = 0; index < graph.AdjacencyList.Vertices[vertex].Count; index++)
                {
                    int neighbor = graph.AdjacencyList.Vertices[vertex][index];
                    int correlatedEdge = graph.AdjacencyList.Edges[vertex][index];
                    double weight = edgesWeights[correlatedEdge];

                    if (d[neighbor] > d[vertex] + weight)
                    {
                        d[neighbor] = d[vertex] + weight;
                        p[neighbor] = vertex;
                    }
                }
            }#1#
        }*/
    }
}
