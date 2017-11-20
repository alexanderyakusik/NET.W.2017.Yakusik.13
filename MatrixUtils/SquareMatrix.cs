namespace MatrixUtils
{
    using System;

    public class SquareMatrix<T> : SquareMatrixPrototype<T>
    {
        #region Private fields

        private readonly T[,] array;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public SquareMatrix(int size) : base(size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(size)} cannot be less than zero.");
            }

            this.array = new T[size, size];
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        protected override T GetElement(int i, int j)
        {
            return this.array[i, j];
        }

        /// <inheritdoc />
        protected override void SetElement(int i, int j, T value)
        {
            this.array[i, j] = value;
        }

        #endregion
    }
}
