using System.Collections.Generic;
using Rhino.Geometry;

namespace MegarachneEngine.Interfaces
{
    public interface ISearch
    {
        Path GetShortestPath(int startVertexIndex, int endVertexIndex);
        List<Point3d> VisitedVertices { get; }
        int[] PreviousArray { get; }
        int[] PreviousEdgeArray { get; }
        bool[] Visited { get; }
        Graph Graph { get; }
    }
}
