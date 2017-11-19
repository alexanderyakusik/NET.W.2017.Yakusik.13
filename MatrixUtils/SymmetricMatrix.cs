namespace MatrixUtils
{
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        #region Ctors

        public SymmetricMatrix(int size) : base(size)
        {
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public override T this[int i, int j]
        {
            get
            {
                return base[i, j];
            }

            set
            {
                base[i, j] = value;
                base[j, i] = value;
            }
        }

        #endregion
    }
}
