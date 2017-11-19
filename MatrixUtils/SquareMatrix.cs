namespace MatrixUtils
{
    using System;

    public class SquareMatrix<T>
    {
        #region Private fields

        private readonly T[,] array;

        #endregion

        #region Ctors

        /// <summary>
        /// Instaniates new square matrix with specified <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        /// <exception cref="ArgumentOutOfRangeException">Size is less than zero.</exception>
        public SquareMatrix(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(size)} cannot be less than zero.");
            }

            array = new T[size, size];
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that is called when the element in the matrix is changed.
        /// </summary>
        public event EventHandler<ElementChangeEventArgs> ElementChange;

        #endregion

        #region Properties
        
        /// <summary>
        /// Square matrix size.
        /// </summary>
        public int Size
        {
            get => array.GetLength(0);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Returns or sets the element in the matrix by <paramref name="i"/> and <paramref name="j"/>.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns>Matrix element at index <paramref name="i"/> and <paramref name="j"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="i"/> is less than zero or greater than or equal to the matrix size.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="j"/> is less than zero or greater than or equal to the matrix size.</exception>
        public virtual T this[int i, int j]
        {
            get
            {
                ValidateArgumentsRange(i, j);

                return array[i, j];
            }

            set
            {
                ValidateArgumentsRange(i, j);

                array[i, j] = value;
                OnElementChange(this, new ElementChangeEventArgs(i, j));
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Method that is called when the element in the matrix is changed.
        /// </summary>
        /// <param name="sender">Object in which the element was changed.</param>
        /// <param name="args">Event arguments</param>
        protected virtual void OnElementChange(object sender, ElementChangeEventArgs args)
        {
            var e = ElementChange;
            e?.Invoke(sender, args);
        }

        #endregion

        #region Private methods

        private void ValidateArgumentsRange(int i, int j)
        {
            if (i < 0 || j < 0 || i >= array.Length || j >= array.Length)
            {
                throw new ArgumentOutOfRangeException($"Index cannot be less than one or more than actual matrix length.");
            }
        }

        #endregion
    }
}
