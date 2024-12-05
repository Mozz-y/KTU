namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Manages a collection of Animal objects, providing methods for adding, accessing, and modifying the collection.
    /// </summary>
    class AnimalContainer
    {
        // Array to store animals
        private Animal[] animals;

        // Number of animals currently in the container
        public int Count { get; private set; }

        // Current capacity of the array
        private int Capacity;

        /// <summary>
        /// Initializes a new instance of animalsContainer with a default capacity of 16.
        /// </summary>
        public AnimalContainer()
        {
            animals = new Animal[16];
            Capacity = 16;
        }

        /// <summary>
        /// Initializes a new instance of animalsContainer with the specified capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity of the container.</param>
        public AnimalContainer(int capacity = 16)
        {
            Capacity = capacity;
            animals = new Animal[capacity];
        }

        /// <summary>
        /// Initializes a new instance of animalsContainer by copying elements from another container.
        /// </summary>
        /// <param name="container">The animalsContainer to copy from.</param>
        public AnimalContainer(AnimalContainer container) : this()
        {
            for (int i = 0; i < container.Count; i++)
            {
                Add(container.Get(i));
            }
        }

        /// <summary>
        /// Adds a new Animal to the container, expanding capacity if necessary.
        /// </summary>
        /// <param name="Animal">The Animal to add.</param>
        public void Add(Animal Animal)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2); // Double the capacity if the array is full
            }
            animals[Count++] = Animal;
        }

        /// <summary>
        /// Retrieves the Animal at the specified index.
        /// </summary>
        /// <param name="index">Index of the Animal to retrieve.</param>
        /// <returns>The Animal at the specified index.</returns>
        public Animal Get(int index)
        {
            return animals[index];
        }

        /// <summary>
        /// Checks if the container contains the specified Animal.
        /// </summary>
        /// <param name="Animal">The Animal to check for.</param>
        /// <returns>True if the Animal is in the container; otherwise, false.</returns>
        public bool Contains(Animal Animal)
        {
            for (int i = 0; i < Count; i++)
            {
                if (animals[i].Equals(Animal))
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
        /// Replaces the Animal at the specified index with a new Animal.
        /// </summary>
        /// <param name="index">Index of the Animal to replace.</param>
        /// <param name="Animal">The new Animal to place at the index.</param>
        public void Put(int index, Animal Animal)
        {
            animals[index] = Animal;
        }

        /// <summary>
        /// Inserts a Animal at the specified index, shifting elements if necessary.
        /// </summary>
        /// <param name="index">Index to insert the Animal at.</param>
        /// <param name="Animal">The Animal to insert.</param>
        public void Insert(int index, Animal Animal)
        {
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2);
            }
            for (int i = Count; i > index; i--)
            {
                animals[i] = animals[i - 1];
            }
            animals[index] = Animal;
            Count++;
        }

        /// <summary>
        /// Removes the specified Animal from the container.
        /// </summary>
        /// <param name="Animal">The Animal to remove.</param>
        /// <returns>True if the Animal was found and removed; otherwise, false.</returns>
        public bool Remove(Animal Animal)
        {
            for (int i = 0; i < Count; i++)
            {
                if (animals[i].Equals(Animal))
                {
                    RemoveAt(i); // Remove Animal at the found index
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the Animal at the specified index.
        /// </summary>
        /// <param name="index">Index of the Animal to remove.</param>
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                animals[i] = animals[i + 1];
            }
            animals[--Count] = null; // Clear the last slot after shifting
        }

        public void Sort()
        {
            Sort(new AnimalsComparator());
        }


        /// <summary>
        /// Sorts the animals in the container using a simple bubble sort algorithm.
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

    /// <summary>
    /// Returns the intersection of the current container with another container.
    /// </summary>
    /// <param name="other">Another animalsContainer to intersect with.</param>
    /// <returns>A new animalsContainer containing animals present in both containers.</returns>
    public AnimalContainer Intersect(AnimalContainer other)
        {
            AnimalContainer result = new AnimalContainer();

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
