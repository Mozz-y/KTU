using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U2_17.Register
{
    internal class EventContainer
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
        public void SortDuplicates()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < Count; j++)
                {
                    if (string.Compare(data[j].Theme, data[minIndex].Theme) < 0 ||
                        (data[j].Theme == data[minIndex].Theme &&
                         string.Compare(data[j].Author, data[minIndex].Author) < 0))
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    var temp = data[i];
                    data[i] = data[minIndex];
                    data[minIndex] = temp;
                }
            }
        }


    }
}
