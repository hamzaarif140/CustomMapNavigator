using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Datatypes
{
    internal class PriorityQueueNode<T>
    {
        // The object being queued
        public T Item { get; set; }
        // The priority value used for ordering (lower = dequeued first)
        public double Priority { get; set; }

        public PriorityQueueNode(T item, double priority)
        {
            Item = item;
            Priority = priority;
        }
    }
}
