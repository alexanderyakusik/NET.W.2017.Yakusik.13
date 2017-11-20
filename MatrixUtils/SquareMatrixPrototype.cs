namespace MatrixUtils
{
    using System;

    public abstract class SquareMatrixPrototype<T>
    {
        #region Ctors

        /// <summary>
        /// Instaniates square matrix with specified <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Size of the square matrix.</param>
        /// <exception cref="ArgumentOutOfRangeException">Size is less than zero.</exception>
        protected SquareMatrixPrototype(int size)
        {
            Size = size > 0 ? size : throw new ArgumentOutOfRangeException($"{nameof(size)} must be greater than or equal to zero.");
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
        public int Size { get; }

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

                return GetElement(i, j);
            }

            set
            {
                ValidateArgumentsRange(i, j);

                SetElement(i, j, value);
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

        /// <summary>
        /// Returns the matrix element at indexes <paramref name="i"/> and <paramref name="j"/>.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <returns>Matrix element.</returns>
        protected abstract T GetElement(int i, int j);

        /// <summary>
        /// Sets the matrix element at indexes <paramref name="i"/> and <paramref name="j"/>.
        /// </summary>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <param name="value">Value to be set</param>
        protected abstract void SetElement(int i, int j, T value);

        #endregion

        #region Private methods

        private void ValidateArgumentsRange(int i, int j)
        {
            if (i < 0 || j < 0 || i >= Size || j >= Size)
            {
                throw new ArgumentOutOfRangeException($"Index cannot be less than one or more than actual matrix length.");
            }
        }

        #endregion
    }
}
