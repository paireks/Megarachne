using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public class AdjacencyList
    {
        public AdjacencyList(int numberOfVertices)
        {
            Vertices = new List<int>[numberOfVertices];
            Edges = new List<int>[numberOfVertices];

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = new List<int>();
                Edges[i] = new List<int>();
            }
        }

        public AdjacencyList(int numberOfVertices, int[,] graphArray)
        {
            Vertices = new List<int>[numberOfVertices];
            Edges = new List<int>[numberOfVertices];

            for (int i = 0; i < Vertices.Length; i++)
            {
                Vertices[i] = new List<int>();
                Edges[i] = new List<int>();
            }

            for (int i = 0; i < graphArray.GetLength(1); i++)
            {
                int firstIndex = graphArray[0, i];
                int secondIndex = graphArray[1, i];

                Vertices[firstIndex].Add(secondIndex);
                Edges[firstIndex].Add(i);
            }
        }

        /*public List<int>[] GetAbsoluteDistancesAdjacencyList(bool empty)
        {
            List<int>[] absoluteDistancesAdjacencyList = new List<int>[Vertices.Length];

            if (!empty)
            {
                
            }

            return absoluteDistancesAdjacencyList;
        }*/

        public List<string> GetStringRepresentation()
        {
            List<string> adjacencyListStringRepresentation = new List<string>();

            foreach (var currentVertexIndexes in Vertices)
            {
                string currentString = "";
                if (currentVertexIndexes != null)
                {
                    foreach (var index in currentVertexIndexes)
                    {
                        currentString += index + ";";
                    }
                }
                adjacencyListStringRepresentation.Add(currentString);
            }

            return adjacencyListStringRepresentation;
        }

        public List<int>[] Vertices { get; set; }
        public List<int>[] Edges { get; set; }
    }
}
