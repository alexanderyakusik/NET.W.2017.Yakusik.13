namespace MatrixUtils
{
    public class SymmetricMatrix<T> : SquareMatrixPrototype<T>
    {
        #region Private fields

        private T[][] array;

        #endregion

        #region Ctors

        /// <inheritdoc />
        public SymmetricMatrix(int size) : base(size)
        {
            this.array = new T[size][];

            for (int i = 0; i < size; i++)
            {
                this.array[i] = new T[i + 1];
            }
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        protected override T GetElement(int i, int j)
        {
            return i < j ? this.array[j][i] : this.array[i][j];
        }

        /// <inheritdoc />
        protected override void SetElement(int i, int j, T value)
        {
            if (i < j)
            {
                this.array[j][i] = value;
                return;
            }

            this.array[i][j] = value;
        }

        #endregion
    }
}
