namespace MatrixUtils
{
    using System;

    public static class MatrixExtensions
    {
        #region Extension methods

        /// <summary>
        /// Performs an addition operation on <paramref name="matrix"/> and <paramref name="otherMatrix"/>.
        /// </summary>
        /// <typeparam name="T">Matrix type.</typeparam>
        /// <typeparam name="TMatrixElementType">Matrix element type.</typeparam>
        /// <param name="matrix">First matrix.</param>
        /// <param name="otherMatrix">Second matrix.</param>
        /// <returns>New matrix with added elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="otherMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Sizes of two matrixes are not equal.</exception>
        public static T Add<T, TMatrixElementType>(
            this T matrix,
            T otherMatrix) where T : SquareMatrixPrototype<TMatrixElementType>
        {
            return matrix.Transform<T, TMatrixElementType>(
                otherMatrix,
                (firstMatrixElement, secondMatrixElement) => (dynamic)firstMatrixElement + (dynamic)secondMatrixElement);
        }

        public static DiagonalMatrix<T> Add<T>(
            this DiagonalMatrix<T> matrix,
            DiagonalMatrix<T> otherMatrix)
        {
            return matrix.Transform(otherMatrix,
                ((firstMatrixElement, secondMatrixElement) =>
                    (dynamic) firstMatrixElement + (dynamic) secondMatrixElement));
        }

        #endregion

        #region Private methods

        private static T Transform<T, TMatrixType>(
            this T matrix,
            T otherMatrix,
            Func<TMatrixType, TMatrixType, TMatrixType> transformFunc) where T : SquareMatrixPrototype<TMatrixType>
        {
            ValidateNullParameters(matrix, otherMatrix, transformFunc);
            ValidateMatrixSizes(matrix, otherMatrix);

            var resultMatrix = (T)Activator.CreateInstance(typeof(T), matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = transformFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static DiagonalMatrix<T> Transform<T>(
            this DiagonalMatrix<T> matrix,
            DiagonalMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            ValidateNullParameters(matrix, otherMatrix, transformFunc);
            ValidateMatrixSizes(matrix, otherMatrix);

            var resultMatrix = new DiagonalMatrix<T>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                resultMatrix[i, i] = transformFunc(matrix[i, i], otherMatrix[i, i]);
            }

            return resultMatrix;
        }

        private static void ValidateNullParameters<T>(
            SquareMatrixPrototype<T> matrix,
            SquareMatrixPrototype<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException($"{nameof(matrix)} cannot be null.");
            }

            if (otherMatrix == null)
            {
                throw new ArgumentNullException($"{nameof(otherMatrix)} cannot be null.");
            }

            if (transformFunc == null)
            {
                throw new ArgumentNullException($"{nameof(transformFunc)} cannot be null.");
            }
        }

        private static void ValidateMatrixSizes<T>(SquareMatrixPrototype<T> matrix, SquareMatrixPrototype<T> otherMatrix)
        {
            if (matrix.Size != otherMatrix.Size)
            {
                throw new ArgumentException($"{nameof(matrix)} and {nameof(otherMatrix)} have different sizes.");
            }
        }

        #endregion
    }
}
