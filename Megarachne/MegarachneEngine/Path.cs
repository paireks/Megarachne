using System.Collections.Generic;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class Path
    {
        public Path(int startVertexIndex, int endVertexIndex, Graph graph, int[] previousArray, int[] previousEdgeArray)
        {
            Vertices = new List<Point3d>();
            Edges = new List<Curve>();
            VerticesIndexes = new List<int>();
            EdgesIndexes = new List<int>();

            int vertexIndex = endVertexIndex;
            int edgeIndex = previousEdgeArray[vertexIndex];

            while (vertexIndex != startVertexIndex)
            {
                Vertices.Add(graph.Vertices[vertexIndex]);
                VerticesIndexes.Add(vertexIndex);

                Edges.Add(graph.Edges[edgeIndex]);
                EdgesIndexes.Add(edgeIndex);

                vertexIndex = previousArray[vertexIndex];
                edgeIndex = previousEdgeArray[vertexIndex];
            }

            Edges.Reverse();
            EdgesIndexes.Reverse();
            Vertices.Reverse();
            VerticesIndexes.Reverse();
        }

        public List<Point3d> Vertices { get; }
        public List<Curve> Edges { get; }
        public List<int> VerticesIndexes { get; }
        public List<int> EdgesIndexes { get; }
    }
}
