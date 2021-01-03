using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegarachneEngine
{
    public sealed class PriorityQueue<TEntry>
        where TEntry : IPrioritizable
    {
        public TEntry Dequeue()
        {
            if (Entries.Any())
            {
                TEntry itemToBeRemoved = Entries.First.Value;
                Entries.RemoveFirst();
                return itemToBeRemoved;
            }

            return default(TEntry);
        }

        public void Enqueue(TEntry entry)
        {
            LinkedListNode<TEntry> value = new LinkedListNode<TEntry>(entry);
            if (Entries.First == null)
            {
                Entries.AddFirst(value);
            }
            else
            {
                LinkedListNode<TEntry> ptr = Entries.First;
                while (ptr.Next != null && ptr.Value.Priority < entry.Priority)
                {
                    ptr = ptr.Next;
                }
                if (ptr.Value.Priority <= entry.Priority)
                {
                    Entries.AddAfter(ptr, value);
                }
                else
                {
                    Entries.AddBefore(ptr, value);
                }
            }
        }

        public LinkedList<TEntry> Entries { get; } = new LinkedList<TEntry>();
        public int Count => Entries.Count;
    }
}
