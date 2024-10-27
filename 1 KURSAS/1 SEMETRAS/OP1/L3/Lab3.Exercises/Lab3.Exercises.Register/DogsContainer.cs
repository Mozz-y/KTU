namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Manages a collection of Dog objects, providing methods for adding, accessing, and modifying the collection.
    /// </summary>
    class DogsContainer
    {
        // Array to store dogs
        private Dog[] dogs;

        // Number of dogs currently in the container
        public int Count { get; private set; }

        // Current capacity of the array
        private int Capacity;

        /// <summary>
        /// Initializes a new instance of DogsContainer with a default capacity of 16.
        /// </summary>
        public DogsContainer()
        {
            dogs = new Dog[16];
            Capacity = 16;
        }

        /// <summary>
        /// Initializes a new instance of DogsContainer with the specified capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity of the container.</param>
        public DogsContainer(int capacity = 16)
        {
            Capacity = capacity;
            dogs = new Dog[capacity];
        }

        /// <summary>
        /// Initializes a new instance of DogsContainer by copying elements from another container.
        /// </summary>
        /// <param name="container">The DogsContainer to copy from.</param>
        public DogsContainer(DogsContainer container) : this()
        {
            for (int i = 0; i < container.Count; i++)
            {
                Add(container.Get(i));
            }
        }

        /// <summary>
        /// Adds a new dog to the container, expanding capacity if necessary.
        /// </summary>
        /// <param name="dog">The dog to add.</param>
        public void Add(Dog dog)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2); // Double the capacity if the array is full
            }
            dogs[Count++] = dog;
        }

        /// <summary>
        /// Retrieves the dog at the specified index.
        /// </summary>
        /// <param name="index">Index of the dog to retrieve.</param>
        /// <returns>The dog at the specified index.</returns>
        public Dog Get(int index)
        {
            return dogs[index];
        }

        /// <summary>
        /// Checks if the container contains the specified dog.
        /// </summary>
        /// <param name="dog">The dog to check for.</param>
        /// <returns>True if the dog is in the container; otherwise, false.</returns>
        public bool Contains(Dog dog)
        {
            for (int i = 0; i < Count; i++)
            {
                if (dogs[i].Equals(dog))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ensures the container has the specified minimum capacity.
        /// </summary>
        /// <param name="minimumCapacity">Minimum capacity to ensure.</param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > Capacity)
            {
                Dog[] temp = new Dog[minimumCapacity];
                for (int i = 0; i < Count; i++)
                {
                    temp[i] = dogs[i];
                }
                Capacity = minimumCapacity;
                dogs = temp;
            }
        }

        /// <summary>
        /// Replaces the dog at the specified index with a new dog.
        /// </summary>
        /// <param name="index">Index of the dog to replace.</param>
        /// <param name="dog">The new dog to place at the index.</param>
        public void Put(int index, Dog dog)
        {
            dogs[index] = dog;
        }

        /// <summary>
        /// Inserts a dog at the specified index, shifting elements if necessary.
        /// </summary>
        /// <param name="index">Index to insert the dog at.</param>
        /// <param name="dog">The dog to insert.</param>
        public void Insert(int index, Dog dog)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2);
            }
            for (int i = Count; i > index; i--)
            {
                dogs[i] = dogs[i - 1];
            }
            dogs[index] = dog;
            Count++;
        }

        /// <summary>
        /// Removes the specified dog from the container.
        /// </summary>
        /// <param name="dog">The dog to remove.</param>
        /// <returns>True if the dog was found and removed; otherwise, false.</returns>
        public bool Remove(Dog dog)
        {
            for (int i = 0; i < Count; i++)
            {
                if (dogs[i].Equals(dog))
                {
                    RemoveAt(i); // Remove dog at the found index
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the dog at the specified index.
        /// </summary>
        /// <param name="index">Index of the dog to remove.</param>
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                dogs[i] = dogs[i + 1];
            }
            dogs[--Count] = null; // Clear the last slot after shifting
        }

        /// <summary>
        /// Sorts the dogs in the container using a simple bubble sort algorithm.
        /// </summary>
        public void Sort()
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < Count - 1; i++)
                {
                    Dog a = dogs[i];
                    Dog b = dogs[i + 1];
                    if (a.CompareTo(b) > 0)
                    {
                        dogs[i] = b;
                        dogs[i + 1] = a;
                        swapped = true;
                    }
                }
            } while (swapped); // Continue sorting until no swaps are needed
        }

        /// <summary>
        /// Returns the intersection of the current container with another container.
        /// </summary>
        /// <param name="other">Another DogsContainer to intersect with.</param>
        /// <returns>A new DogsContainer containing dogs present in both containers.</returns>
        public DogsContainer Intersect(DogsContainer other)
        {
            DogsContainer result = new DogsContainer();

            // Add dogs to result if they are in both containers
            for (int i = 0; i < Count; i++)
            {
                Dog current = dogs[i];
                if (other.Contains(current))
                {
                    result.Add(current);
                }
            }

            return result;
        }
    }
}
