using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Datatypes
{
    internal class MinPriorityQueue<T>
    {
        // The binary‑heap storage
        private List<PriorityQueueNode<T>> heap = new List<PriorityQueueNode<T>>();
        // Where each item is in the heap
        private Dictionary<T, int> positions = new Dictionary<T, int>();

        public int Count => heap.Count;

        // Add a new item with the given priority
        public void Enqueue(T item, double priority)
        {
            var node = new PriorityQueueNode<T>(item, priority);
            heap.Add(node);
            int index = heap.Count - 1;
            positions[item] = index;
            BubbleUp(index);
        }

        // Remove and return the item with the lowest priority
        public T Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");

            T minItem = heap[0].Item;
            Swap(0, heap.Count - 1);
            heap.RemoveAt(heap.Count - 1);
            positions.Remove(minItem);

            if (heap.Count > 0)
                BubbleDown(0);

            return minItem;
        }

        // If an item is already in the queue, change its priority
        public void UpdatePriority(T item, double newPriority)
        {
            if (!positions.TryGetValue(item, out int index))
                throw new InvalidOperationException("Item not found in the priority queue.");

            double oldPriority = heap[index].Priority;
            heap[index].Priority = newPriority;

            // If the new priority is smaller, it may need to move up
            if (newPriority < oldPriority)
                BubbleUp(index);
            else
                BubbleDown(index);
        }

        // Check if an item is currently queued
        public bool Contains(T item)
        {
            return positions.ContainsKey(item);
        }

        // Move a node up until heap property is restored
        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[index].Priority < heap[parent].Priority)
                {
                    Swap(index, parent);
                    index = parent;
                }
                else
                {
                    break;
                }
            }
        }

        // Move a node down until heap property is restored
        private void BubbleDown(int index)
        {
            int last = heap.Count - 1;

            while (true)
            {
                int left = index * 2 + 1;
                int right = index * 2 + 2;
                int smallest = index;

                if (left <= last && heap[left].Priority < heap[smallest].Priority)
                    smallest = left;
                if (right <= last && heap[right].Priority < heap[smallest].Priority)
                    smallest = right;

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;
                }
                else
                {
                    break;
                }
            }
        }

        // Swap two nodes in the heap and update their positions
        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;

            positions[heap[i].Item] = i;
            positions[heap[j].Item] = j;
        }
    }
}
