namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Manages a collection of Dog objects, providing methods for adding, accessing, and modifying the collection.
    /// </summary>
    class AnimalsContainer
    {
        // Array to store dogs
        private Animal[] animals;

        // Number of dogs currently in the container
        public int Count { get; private set; }

        // Current capacity of the array
        private int Capacity;

        /// <summary>
        /// Initializes a new instance of DogsContainer with a default capacity of 16.
        /// </summary>
        public AnimalsContainer()
        {
            animals = new Animal[16];
            Capacity = 16;
        }

        /// <summary>
        /// Initializes a new instance of DogsContainer with the specified capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity of the container.</param>
        public AnimalsContainer(int capacity = 16)
        {
            Capacity = capacity;
            animals = new Animal[capacity];
        }

        /// <summary>
        /// Initializes a new instance of DogsContainer by copying elements from another container.
        /// </summary>
        /// <param name="container">The DogsContainer to copy from.</param>
        public AnimalsContainer(AnimalsContainer container) : this()
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
        public void Add(Animal animal)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2); // Double the capacity if the array is full
            }
            animals[Count++] = animal;
        }

        /// <summary>
        /// Retrieves the dog at the specified index.
        /// </summary>
        /// <param name="index">Index of the dog to retrieve.</param>
        /// <returns>The dog at the specified index.</returns>
        public Animal Get(int index)
        {
            return animals[index];
        }

        /// <summary>
        /// Checks if the container contains the specified dog.
        /// </summary>
        /// <param name="dog">The dog to check for.</param>
        /// <returns>True if the dog is in the container; otherwise, false.</returns>
        public bool Contains(Animal animal)
        {
            for (int i = 0; i < Count; i++)
            {
                if (animals[i].Equals(animal))
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
                Animal[] temp = new Animal[minimumCapacity];
                for (int i = 0; i < Count; i++)
                {
                    temp[i] = animals[i];
                }
                Capacity = minimumCapacity;
                animals = temp;
            }
        }

        /// <summary>
        /// Replaces the dog at the specified index with a new dog.
        /// </summary>
        /// <param name="index">Index of the dog to replace.</param>
        /// <param name="dog">The new dog to place at the index.</param>
        public void Put(int index, Animal animal)
        {
            animals[index] = animal;
        }

        /// <summary>
        /// Inserts a dog at the specified index, shifting elements if necessary.
        /// </summary>
        /// <param name="index">Index to insert the dog at.</param>
        /// <param name="dog">The dog to insert.</param>
        public void Insert(int index, Animal animal)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2);
            }
            for (int i = Count; i > index; i--)
            {
                animals[i] = animals[i - 1];
            }
            animals[index] = animal;
            Count++;
        }

        /// <summary>
        /// Removes the specified dog from the container.
        /// </summary>
        /// <param name="dog">The dog to remove.</param>
        /// <returns>True if the dog was found and removed; otherwise, false.</returns>
        public bool Remove(Animal animal)
        {
            for (int i = 0; i < Count; i++)
            {
                if (animals[i].Equals(animal))
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
                animals[i] = animals[i + 1];
            }
            animals[--Count] = null; // Clear the last slot after shifting
        }

        /// <summary>
        /// Sorts the dogs in the container using a simple bubble sort algorithm.
        /// </summary>
        public void Sort(AnimalsComparator comparator)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    Animal a = this.animals[i];
                    Animal b = this.animals[i + 1];
                    if (comparator.Compare(a, b) > 0)
                    {
                        this.animals[i] = b;
                        this.animals[i + 1] = a;
                        flag = true;
                    }
                }
            }
        }

        public void Sort()
        {
            Sort(new AnimalsComparator());
        }

        /// <summary>
        /// Returns the intersection of the current container with another container.
        /// </summary>
        /// <param name="other">Another animalsContainer to intersect with.</param>
        /// <returns>A new animalsContainer containing animals present in both containers.</returns>
        public AnimalsContainer Intersect(AnimalsContainer other)
        {
            AnimalsContainer result = new AnimalsContainer();

            // Add animals to result if they are in both containers
            for (int i = 0; i < Count; i++)
            {
                Animal current = animals[i];
                if (other.Contains(current))
                {
                    result.Add(current);
                }
            }

            return result;
        }
    }
}
