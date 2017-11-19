namespace MatrixUtils
{
    using System;

    public static class MatrixExtensions
    {
        #region Extension methods

        /// <summary>
        /// Performs an addition operation on <paramref name="matrix"/> and <paramref name="otherMatrix"/>.
        /// </summary>
        /// <typeparam name="T">Matrix element type.</typeparam>
        /// <param name="matrix">First matrix.</param>
        /// <param name="otherMatrix">Second matrix.</param>
        /// <returns>New SquareMatrix with added elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="otherMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Sizes of two matrixes are not equal.</exception>
        public static SquareMatrix<T> Add<T>(
            this SquareMatrix<T> matrix,
            SquareMatrix<T> otherMatrix)
        {
            return matrix.Transform(
                otherMatrix,
                (firstMatrixElement, secondMatrixElement) => (dynamic)firstMatrixElement + (dynamic)secondMatrixElement);
        }

        #endregion

        #region Private methods

        private static SquareMatrix<T> Transform<T>(
            this SquareMatrix<T> matrix,
            SquareMatrix<T> otherMatrix,
            Func<T, T, T> addFunc)
        {
            ValidateNullParameters(matrix, otherMatrix, addFunc);
            ValidateMatrixSizes(matrix, otherMatrix);

            var resultMatrix = new SquareMatrix<T>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = addFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static void ValidateNullParameters<T>(
            SquareMatrix<T> matrix,
            SquareMatrix<T> otherMatrix,
            Func<T, T, T> addFunc)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException($"{nameof(matrix)} cannot be null.");
            }

            if (otherMatrix == null)
            {
                throw new ArgumentNullException($"{nameof(otherMatrix)} cannot be null.");
            }

            if (addFunc == null)
            {
                throw new ArgumentNullException($"{nameof(addFunc)} cannot be null.");
            }
        }

        private static void ValidateMatrixSizes<T>(SquareMatrix<T> matrix, SquareMatrix<T> otherMatrix)
        {
            if (matrix.Size != otherMatrix.Size)
            {
                throw new ArgumentException($"{nameof(matrix)} and {nameof(otherMatrix)} have different sizes.");
            }
        }

        #endregion
    }
}
