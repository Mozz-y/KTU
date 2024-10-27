using System;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Class that represents a container for storing students.
    /// </summary>
    class StudentsContainer
    {
        private Student[] students;  // Array to store Student objects
        public int Count { get; private set; }  // Number of students currently in the container
        private int Capacity;  // Current capacity of the container

        /// <summary>
        /// Default constructor initializes the container with a default capacity.
        /// </summary>
        public StudentsContainer()
        {
            Capacity = 16;  // Default capacity
            students = new Student[Capacity];  // Initialize the array
        }

        /// <summary>
        /// Adds a student to the container.
        /// </summary>
        /// <param name="student">The student to be added.</param>
        public void Add(Student student)
        {
            // Check if there is enough capacity
            if (Count == Capacity)
            {
                EnsureCapacity(Capacity * 2);  // Double the capacity
            }
            students[Count++] = student;  // Add the student and increment the count
        }

        /// <summary>
        /// Gets the student at a specified index.
        /// </summary>
        /// <param name="index">The index of the student to retrieve.</param>
        /// <returns>The student at the specified index.</returns>
        public Student Get(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Nurodytas indeksas yra neleistinas.");  // Invalid index exception
            }
            return students[index];
        }

        /// <summary>
        /// Checks if a student exists in the container.
        /// </summary>
        /// <param name="student">The student to check for.</param>
        /// <returns>True if the student exists, otherwise false.</returns>
        public bool Contains(Student student)
        {
            for (int i = 0; i < Count; i++)
            {
                if (students[i].Equals(student))
                {
                    return true;  // Student found
                }
            }
            return false;  // Student not found
        }

        /// <summary>
        /// Ensures that the container has enough capacity to hold more students.
        /// </summary>
        /// <param name="minimumCapacity">The minimum required capacity.</param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > Capacity)
            {
                Student[] temp = new Student[minimumCapacity];  // Create a new array with the new capacity
                for (int i = 0; i < Count; i++)
                {
                    temp[i] = students[i];  // Copy existing students to the new array
                }
                students = temp;  // Replace the old array with the new one
                Capacity = minimumCapacity;  // Update capacity
            }
        }

        /// <summary>
        /// Sorts the students based on their average grades (descending).
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = i + 1; j < Count; j++)
                {
                    if (students[i].Average < students[j].Average)
                    {
                        // Swap students[i] and students[j]
                        Student temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                    else if (students[i].Average == students[j].Average &&
                             string.Compare(students[i].LastName, students[j].LastName) > 0)
                    {
                        // If averages are equal, sort by last name
                        Student temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                }
            }
        }
    }
}
