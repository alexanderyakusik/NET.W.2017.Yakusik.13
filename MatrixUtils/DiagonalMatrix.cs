namespace MatrixUtils
{
    using System;

    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        #region Ctors

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="size"></param>
        public DiagonalMatrix(int size) : base(size)
        {
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <exception cref="InvalidOperationException"><paramref name="i"/> is not equal to <paramref name="j"/>.</exception>
        public override T this[int i, int j]
        {
            get
            {
                return base[i, j];
            }

            set
            {
                if (i != j)
                {
                    throw new InvalidOperationException($"Cannot set elements that are not on the main diagonal.");
                }

                base[i, j] = value;
            }
        }

        #endregion
    }
}
