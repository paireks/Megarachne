using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public class AStarVertex : IEquatable<AStarVertex>
    {
        public AStarVertex(int index, double weight, double straightLineDistanceToEnd)
        {
            Index = index;
            Weight = weight;
            StraightLineDistanceToEnd = straightLineDistanceToEnd;
        }

        public bool Equals(AStarVertex other)
        {
            return Index == other.Index;
        }

        public int Index { get; }
        public double Weight { get; set; }
        public double StraightLineDistanceToEnd { get; }

    }
}
