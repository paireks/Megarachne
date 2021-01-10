using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public class DijkstraVertex
    {
        public DijkstraVertex(int index, double weight)
        {
            Index = index;
            Weight = weight;
        }

        public int Index { get; }
        public double Weight { get; }
    }
}
