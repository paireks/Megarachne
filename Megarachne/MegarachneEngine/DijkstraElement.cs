using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public class DijkstraElement: IPrioritizable
    {
        public DijkstraElement(int vertexIndex, double priority, int previousVertexIndex, bool isDone)
        {
            VertexIndex = vertexIndex;
            Priority = priority;
            PreviousVertexIndex = previousVertexIndex;
            IsDone = isDone;
        }

        public int VertexIndex { get; }

        public double Priority { get; set; }

        public int PreviousVertexIndex { get; set; }

        public bool IsDone { get; set; }
    }
}
