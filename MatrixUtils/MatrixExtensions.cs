using Microsoft.CSharp.RuntimeBinder;

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
        /// <returns>New matrix with added elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="otherMatrix"/> is null.</exception>
        /// <exception cref="ArgumentException">Sizes of two matrixes are not equal.</exception>
        /// <exception cref="NotSupportedException">Matrix element type does not override plus operator.</exception>
        public static SquareMatrixPrototype<T> Add<T>(
            this SquareMatrixPrototype<T> matrix,
            SquareMatrixPrototype<T> otherMatrix)
        {
            ValidateNullParameters(matrix, otherMatrix);
            ValidateMatrixSizes(matrix, otherMatrix);

            Func<T, T, T> f = (x, y) => (dynamic)x + (dynamic)y;

            try
            {
                return Transform((dynamic)matrix, (dynamic)otherMatrix, f);
            }
            catch (RuntimeBinderException)
            {
                throw new NotSupportedException($"Cannot add instances of this type.");
            }
        }

        #endregion

        #region Private methods

        #region Transform versions

        private static SquareMatrix<T> Transform<T>(
            SquareMatrix<T> matrix,
            SquareMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<SquareMatrix<T>>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = transformFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static SymmetricMatrix<T> Transform<T>(
            SymmetricMatrix<T> matrix,
            SymmetricMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<SymmetricMatrix<T>>(matrix.Size);

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
            DiagonalMatrix<T> matrix,
            DiagonalMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<DiagonalMatrix<T>>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                resultMatrix[i, i] = transformFunc(matrix[i, i], otherMatrix[i, i]);
            }

            return resultMatrix;
        }

        private static SymmetricMatrix<T> Transform<T>(
            SymmetricMatrix<T> matrix,
            DiagonalMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<SymmetricMatrix<T>>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = transformFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static SymmetricMatrix<T> Transform<T>(
            DiagonalMatrix<T> matrix,
            SymmetricMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            return Transform(otherMatrix, matrix, transformFunc);
        }

        private static SquareMatrix<T> Transform<T>(
            SquareMatrix<T> matrix,
            DiagonalMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<SquareMatrix<T>>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = transformFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static SquareMatrix<T> Transform<T>(
            DiagonalMatrix<T> matrix,
            SquareMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            return Transform(otherMatrix, matrix, transformFunc);
        }

        private static SquareMatrix<T> Transform<T>(
            SquareMatrix<T> matrix,
            SymmetricMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            var resultMatrix = CreateMatrix<SquareMatrix<T>>(matrix.Size);

            for (int i = 0; i < resultMatrix.Size; i++)
            {
                for (int j = 0; j < resultMatrix.Size; j++)
                {
                    resultMatrix[i, j] = transformFunc(matrix[i, j], otherMatrix[i, j]);
                }
            }

            return resultMatrix;
        }

        private static SquareMatrix<T> Transform<T>(
            SymmetricMatrix<T> matrix,
            SquareMatrix<T> otherMatrix,
            Func<T, T, T> transformFunc)
        {
            return Transform(otherMatrix, matrix, transformFunc);
        }

        #endregion

        #region Validation methods

        private static void ValidateNullParameters<T>(
            SquareMatrixPrototype<T> matrix,
            SquareMatrixPrototype<T> otherMatrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException($"{nameof(matrix)} cannot be null.");
            }

            if (otherMatrix == null)
            {
                throw new ArgumentNullException($"{nameof(otherMatrix)} cannot be null.");
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

        #region Factory method

        private static T CreateMatrix<T>(int size)
        {
            return (T)Activator.CreateInstance(typeof(T), size);
        }

        #endregion

        #endregion
    }
}
