using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MegarachneEngine
{
    public class Graph
    {
        public Graph(List<GraphPart> graphParts, double tolerance)
        {
            Vertices = new List<Point3d>();
            Edges = new Curve[graphParts.Count];
            GraphArray = new int[graphParts.Count, 2];

            for (int i = 0; i < graphParts.Count; i++)
            {
                Edges[i] = graphParts[i].Edge;

                
            }

        }

        public int[,] GraphArray { get; set; }
        public Curve[] Edges { get; set; }
        public List<Point3d> Vertices { get; set; }
    }
}
