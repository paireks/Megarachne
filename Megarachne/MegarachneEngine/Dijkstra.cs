

namespace MegarachneEngine
{
    public static class Dijkstra
    {
        private static DijkstraElement[] SetStartingDijkstraElementsArray(Graph graph, int startVertexIndex)
        {
            DijkstraElement[] array = new DijkstraElement[graph.Vertices.Count];

            for (int i = 0; i < startVertexIndex; i++)
            {
                array[i] = new DijkstraElement(i, double.MaxValue, -1, -1, false);
            }

            array[startVertexIndex] = new DijkstraElement(startVertexIndex, 0, -1, -1, false);

            for (int i = startVertexIndex + 1; i < graph.Vertices.Count; i++)
            {
                array[i] = new DijkstraElement(i, double.MaxValue, -1, -1, false);
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

        public static Path GetShortestPath(Graph graph, int startVertexIndex, int endVertexIndex)
        {
            DijkstraElement[] arrayOfDijkstraElements = SetStartingDijkstraElementsArray(graph, startVertexIndex);
            double[] edgesWeights = graph.GetEdgesWeights();

            PriorityQueue<DijkstraElement> priorityQueue = new PriorityQueue<DijkstraElement>();

            priorityQueue.Enqueue(arrayOfDijkstraElements[startVertexIndex]);

            bool keepSearching = true;
            while (priorityQueue.Count != 0 || keepSearching)
            {
                DijkstraElement currentVertex = priorityQueue.Dequeue();

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
                        arrayOfDijkstraElements[neighborIndex].PreviousEdgeIndex = edgeToNeighbor;


                        priorityQueue.Enqueue(arrayOfDijkstraElements[neighborIndex]);
                    }
                }
                arrayOfDijkstraElements[currentVertex.VertexIndex].IsDone = true;
                if (currentVertex.VertexIndex == endVertexIndex)
                {
                    keepSearching = false;
                }
            }

            Path shortestPath = new Path();

            int currentVertexIndex = endVertexIndex;
            int edgeIndex = 0;

            while (arrayOfDijkstraElements[currentVertexIndex].PreviousVertexIndex != startVertexIndex)
            {
                edgeIndex = arrayOfDijkstraElements[currentVertexIndex].PreviousEdgeIndex;

                shortestPath.Edges.Add(graph.Edges[edgeIndex]);
                shortestPath.EdgesIndexes.Add(edgeIndex);

                shortestPath.Vertices.Add(graph.Vertices[currentVertexIndex]);
                shortestPath.VerticesIndexes.Add(currentVertexIndex);

                currentVertexIndex = arrayOfDijkstraElements[currentVertexIndex].PreviousVertexIndex;
            }

            shortestPath.Edges.Add(graph.Edges[edgeIndex]);
            shortestPath.EdgesIndexes.Add(edgeIndex);

            shortestPath.Vertices.Add(graph.Vertices[currentVertexIndex]);
            shortestPath.VerticesIndexes.Add(currentVertexIndex);

            shortestPath.Edges.Reverse();
            shortestPath.EdgesIndexes.Reverse();
            shortestPath.Vertices.Reverse();
            shortestPath.VerticesIndexes.Reverse();

            return shortestPath;
        }
    }
}
