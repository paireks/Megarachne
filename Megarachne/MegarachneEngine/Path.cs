using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace MegarachneEngine
{
    public class Path
    {
        public Path()
        {
            Vertices = new List<Point3d>();
            Edges = new List<Curve>();
            VerticesIndexes = new List<int>();
            EdgesIndexes = new List<int>();
        }

        public List<Point3d> Vertices { get; set; }
        public List<Curve> Edges { get; set; }
        public List<int> VerticesIndexes { get; set; }
        public List<int> EdgesIndexes { get; set; }
    }
}
