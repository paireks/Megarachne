using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public class DijkstraVertex : IEquatable<DijkstraVertex>
    {
        public DijkstraVertex(int index, double weight)
        {
            Index = index;
            Weight = weight;
        }

        public bool Equals(DijkstraVertex other)
        {
            return Index == other.Index;
        }

        public int Index { get; }
        public double Weight { get; set; }
    }
}
