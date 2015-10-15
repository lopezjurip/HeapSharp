using System;

namespace heap_sharp
{
    public interface IHeapeable
    {
        int key { get; set; }
    }

    public class Heap<T> where T : IHeapeable
    {
        private T[] Items { get; set; }
        public int HeapSize { get; private set; }

        public Heap(int maxCapacity)
        {
            this.Items = new T[maxCapacity];
            this.HeapSize = 0;
        }

        public static Heap<T> Build(T[] items)
        {
            var heap = new Heap<T>(items.Length);
            heap.Items = items;
            for (var level = items.Length / 2; level >= 0; level--)
            {
                heap.Heapify(level);
            }
            return heap;
        }

        public T Max()
        {
            return this.Items[0];
        }

        public T TakeMax()
        {
            var max = this.Max();
            this.Items[0] = this.Items[HeapSize - 1];
            this.HeapSize -= 1;
            this.Heapify(0);
            return max;
        }

        private void Heapify(int index)
        {
            var left = 2 * index;
            var right = 2 * index + 1;
            var toMove = index;

            if (right < this.HeapSize && this.Items[right].key > this.Items[index].key)
            {
                toMove = right;
            }
            else if (left < this.HeapSize && this.Items[left].key > this.Items[index].key)
            {
                toMove = left;
            }

            if (toMove != index)
            {
                Heap<T>.Swap(this.Items, index, toMove);
                this.Heapify(toMove);
            }
        }

        public void IncrementKey(int index, int value)
        {
            this.Items[index].key = value;
            while (index > 0 && this.Items[index / 2].key < this.Items[index].key)
            {
                Heap<T>.Swap(this.Items, index, index / 2);
                index /= 2;
            }
        }

        public void Insert(T item)
        {
            this.HeapSize += 1;
            this.Items[this.HeapSize - 1] = item;
            this.IncrementKey(this.HeapSize - 1, item.key);
        }

        public void HeapSort()
        {
            for (int l = this.Items.Length; l >= 1; l--)
            {
                Heap<T>.Swap(this.Items, 0, l);
                this.HeapSize -= 1;
                this.Heapify(1);
            }
        }

        static void Swap(T[] array, int a, int b)
        {
            var temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
    }
}
