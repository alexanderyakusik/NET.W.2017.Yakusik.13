namespace MatrixUtils
{
    using System;

    public class DiagonalMatrix<T> : SquareMatrixPrototype<T>
    {
        #region Private fields

        private T[] array;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public DiagonalMatrix(int size) : base(size)
        {
            this.array = new T[size];
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        protected override T GetElement(int i, int j)
        {
            return i != j ? default(T) : this.array[i];
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="i"/> is not equal to <paramref name="j"/>.</exception>
        protected override void SetElement(int i, int j, T value)
        {
            if (i != j)
            {
                throw new InvalidOperationException($"Cannot set elements that are not on main diagonal.");
            }

            this.array[i] = value;
        }

        #endregion
    }
}
