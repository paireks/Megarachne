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
        private static DijkstraElement[] SetStartingDijkstraElementsArray(Graph graph, int startVertexIndex)
        {
            DijkstraElement[] array = new DijkstraElement[graph.Vertices.Count];

            for (int i = 0; i < startVertexIndex; i++)
            {
                array[i] = new DijkstraElement(i, double.MaxValue, -1, false);
            }

            array[startVertexIndex] = new DijkstraElement(startVertexIndex, 0, -1, false);

            for (int i = startVertexIndex + 1; i < graph.Vertices.Count; i++)
            {
                array[i] = new DijkstraElement(i, double.MaxValue, -1, false);
            }

            return array;
        } 

        public static DijkstraElement[] Search(Graph graph, int startVertexIndex)
        {
            DijkstraElement[] arrayOfDijkstraElements = SetStartingDijkstraElementsArray(graph, startVertexIndex);
            double[] edgesWeights = graph.GetEdgesWeights();
            
            PriorityQueue<DijkstraElement> priorityQueue = new PriorityQueue<DijkstraElement>();

            priorityQueue.Enqueue(arrayOfDijkstraElements[startVertexIndex]);

            while (priorityQueue.Count != 0)
            {
                DijkstraElement currentVertex = priorityQueue.Dequeue();

                if (currentVertex.IsDone)
                {
                    continue;
                }

                for (int i = 0; i < graph.AdjacencyList.Vertices[currentVertex.VertexIndex].Count; i++)
                {
                    int neighborIndex = graph.AdjacencyList.Vertices[currentVertex.VertexIndex][i];

                    if (arrayOfDijkstraElements[neighborIndex].IsDone)
                    {
                        continue;
                    }

                    int edgeToNeighbor = graph.AdjacencyList.Edges[currentVertex.VertexIndex][i];
                    double weightToNeighbor = edgesWeights[edgeToNeighbor];

                    if (arrayOfDijkstraElements[neighborIndex].Priority > arrayOfDijkstraElements[currentVertex.VertexIndex].Priority + weightToNeighbor)
                    {
                        arrayOfDijkstraElements[neighborIndex].Priority =
                            arrayOfDijkstraElements[currentVertex.VertexIndex].Priority + weightToNeighbor;

                        arrayOfDijkstraElements[neighborIndex].PreviousVertexIndex = currentVertex.VertexIndex;

                        priorityQueue.Enqueue(arrayOfDijkstraElements[neighborIndex]);
                    }
                }
                arrayOfDijkstraElements[currentVertex.VertexIndex].IsDone = true;
            }

            return arrayOfDijkstraElements;
        }
    }
}
