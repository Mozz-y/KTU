using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_17.Register
{
    class EventContainer
    {
        private Info[] data;
        public int Count { get; private set; }
        private int Capacity;

        public EventContainer(int capacity = 16)
        {
            Capacity = capacity;
            data = new Info[capacity];
        }

        public Info Get(int index)
        {
            return data[index];
        }

        public void Add(Info info)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2);
            }
            data[Count++] = info;
        }

        public void Put(int index, Info info)
        {
            data[index] = info;
        }

        public void Insert(int index, Info info)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Count * 2);
            }

            for (int i = Count; i > index; i--)
            {
                data[i] = data[i - 1];
            }
            data[index] = info;
            Count++;
        }

        public bool Remove(Info info)
        {
            for (int i = 0; i < Count; i++)
            {
                if (data[i].Equals(info))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            data[--Count] = null;
        }

        public bool Contains(Info info)
        {
            for (int i = 0; i < Count; i++)
            {
                if (data[i].Equals(info))
                {
                    return true;
                }
            }
            return false;
        }

        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > Capacity)
            {
                Info[] temp = new Info[minimumCapacity];
                for (int i = 0; i < Count; i++)
                {
                    temp[i] = data[i];
                }
                Capacity = minimumCapacity;
                data = temp;
            }
        }

        // Add a method to sort by Theme and Author
        public void SortDuplicates(List<Info> duplicates)
        {
            // Selection sort implementation for sorting by theme and author
            for (int i = 0; i < duplicates.Count - 1; i++)
            {
                int minIndex = i; // Assume the minimum is the first unsorted element

                // Find the index of the smallest element in the unsorted part of the list
                for (int j = i + 1; j < duplicates.Count; j++)
                {
                    // Compare by theme first; if themes are the same, compare by author
                    if (string.Compare(duplicates[j].Theme, duplicates[minIndex].Theme) < 0 ||
                        (duplicates[j].Theme == duplicates[minIndex].Theme &&
                         string.Compare(duplicates[j].Author, duplicates[minIndex].Author) < 0))
                    {
                        minIndex = j; // Update the index of the minimum element
                    }
                }

                // Swap the found minimum element with the first unsorted element
                if (minIndex != i)
                {
                    var temp = duplicates[i];
                    duplicates[i] = duplicates[minIndex];
                    duplicates[minIndex] = temp;
                }
            }
        }

    }
}
