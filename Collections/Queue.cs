namespace Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IEnumerable<T>
    {
        #region Private fields

        private const int DefaultCapacity = 4;

        private T[] array;
        private int headIndex, tailIndex, size, version;

        #endregion

        #region Ctors

        /// <summary>
        /// Instaniates queue with setting capacity parameter.
        /// </summary>
        /// <param name="capacity">Initial queue capacity.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than one.</exception>
        public Queue(int capacity = DefaultCapacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(capacity)} cannot be less than one.");
            }

            array = new T[capacity];
        }

        /// <summary>
        /// Instaniates queue with filling the items from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Source collection from which the elements are enqueued.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is null.</exception>
        public Queue(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException($"{nameof(collection)} cannot be null.");
            }

            array = new T[DefaultCapacity];

            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Amount of elements in the queue.
        /// </summary>
        public int Length
        {
            get => size;
        }

        /// <summary>
        /// Current queue capacity.
        /// </summary>
        public int Capacity
        {
            get => array.Length;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enqueues <paramref name="item"/>.
        /// </summary>
        /// <param name="item">Item to be enqueued.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public void Enqueue(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"{nameof(item)} cannot be null.");
            }

            if (Length == Capacity)
            {
                ResizeArray();
            }

            array[tailIndex] = item;
            tailIndex = (tailIndex + 1) % Capacity;
            size++;
            version++;
        }

        /// <summary>
        /// Dequeues first item.
        /// </summary>
        /// <returns>First item from the queue.</returns>
        /// <exception cref="InvalidOperationException">Size of the queue is zero.</exception>
        public T Dequeue()
        {
            if (size == 0)
            {
                throw new InvalidOperationException($"Cannot dequeue element when the queue is empty.");
            }

            T result = array[headIndex];
            array[headIndex] = default(T);
            headIndex = (headIndex + 1) % Capacity;
            size--;
            version--;

            return result;
        }

        #endregion

        #region Interfaces implementation

        #region IEnumerable<T>

        /// <summary>
        /// Returns queue enumerator.
        /// </summary>
        /// <returns>Queue enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns queue enumerator.
        /// </summary>
        /// <returns>Queue enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #endregion

        #region Private methods

        private void ResizeArray()
        {
            var newArray = new T[Capacity * 2];
            for (int i = 0; i < size; i++)
            {
                newArray[i] = array[(i + headIndex) % Capacity];
            }

            array = newArray;
            headIndex = 0;
            tailIndex = Length;
        }

        #endregion

        #region Enumerator

        /// <summary>
        /// Custom queue enumerator.
        /// </summary>
        internal struct Enumerator : IEnumerator<T>
        {
            private Queue<T> queue;
            private int currentIndex, initialVersion;

            /// <summary>
            /// Instaniates an enumerator for the specified <paramref name="queue"/>.
            /// </summary>
            /// <param name="queue">Queue to be enumerated.</param>
            /// <exception cref="ArgumentNullException"><paramref name="queue"/> is null.</exception>
            public Enumerator(Queue<T> queue)
            {
                this.queue = queue ?? throw new ArgumentNullException($"{nameof(queue)} cannot be null.");
                currentIndex = queue.headIndex - 1;
                initialVersion = queue.version;
            }

            /// <summary>
            /// Returns current element of the enumerator.
            /// </summary>
            public T Current
            {
                get
                {
                    return queue.array[currentIndex];
                }
            }

            /// <summary>
            /// Returns current element of the enumerator.
            /// </summary>
            object IEnumerator.Current
            {
                get => Current;
            }

            /// <summary>
            /// Performs actions to destroy the state of the object.
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Moves the current element to the next one.
            /// </summary>
            /// <returns>True, if there are elements left. Otherwise, returns false.</returns>
            /// <exception cref="InvalidOperationException">Collection has been changed between the enumerations.</exception>
            public bool MoveNext()
            {
                if (queue.version != initialVersion)
                {
                    throw new InvalidOperationException("The collection has been changed.");
                }

                currentIndex = (currentIndex + 1) % queue.Capacity;
                return currentIndex != queue.tailIndex;
            }

            /// <summary>
            /// Resets the current element to the first one.
            /// </summary>
            public void Reset()
            {
                currentIndex = queue.headIndex - 1;
            }
        }

        #endregion
    }
}
