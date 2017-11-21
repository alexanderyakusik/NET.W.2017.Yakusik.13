namespace MatrixUtils.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class SquareMatrixTests
    {
        [TestCase(-1)]
        public void Ctor_InvalidSizePassed_ArgumentOutOfRangeExceptionThrown(int size)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SquareMatrix<int>(size));
        }

        [TestCase(4, -1, 3)]
        [TestCase(2, 1, 5)]
        public void Indexer_InvalidIndexesPassed_ArgumentOutOfRangeExceptionThrown(int matrixSize, int i, int j)
        {
            var matrix = new SquareMatrix<int>(matrixSize);

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix[i, j] = 0);
        }

        [TestCase(5, 10, 4, 4)]
        [TestCase(5, -2, 3, 4)]
        public void SymmetricMatrix_ElementsChanged_TransponeElementsChanges(int matrixSize, int element, int i, int j)
        {
            var matrix = new SymmetricMatrix<int>(matrixSize);

            matrix[i, j] = element;

            Assert.AreEqual(matrix[i, j], matrix[j, i]);
        }

        [TestCase(5, 10, 1, 1)]
        [TestCase(5, -1, 0, 1)]
        public void DiagonalMatrix_ElementsChanged_WorksCorrectlyDependingOnIndexes(
            int matrixSize, 
            int element, 
            int i,
            int j)
        {
            var matrix = new DiagonalMatrix<int>(matrixSize);

            if (i == j)
            {
                matrix[i, j] = element;
                Assert.AreEqual(element, matrix[i, j]);
                return;
            }

            Assert.Throws<InvalidOperationException>(() => matrix[i, j] = element);
        }

        [Test]
        public void Add_NullMatrixPassed_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new SquareMatrix<int>(1).Add(null));
        }

        [Test]
        public void Add_MatrixesWithDifferentSizesPassed_ArgumentExceptionThorwn()
        {
            Assert.Throws<ArgumentException>(() => new SquareMatrix<int>(3).Add(new SquareMatrix<int>(2)));
        }

        [Test]
        public void Add_CorrectMatrixesPassed_WorksCorrectly()
        {
            var lhs = new DiagonalMatrix<int>(2);
            lhs[0, 0] = 1;
            lhs[1, 1] = 2;

            var rhs = new DiagonalMatrix<int>(2);
            rhs[0, 0] = -100;
            rhs[1, 1] = 50;

            var result = new DiagonalMatrix<int>(2);
            result[0, 0] = -99;
            result[1, 1] = 52;

            var actual = lhs.Add(rhs);

            Assert.AreEqual(result[0, 0], actual[0, 0]);
            Assert.AreEqual(result[1, 1], actual[1, 1]);
        }

        [Test]
        public void Add_SymmetricalAndDiagonalMatrixes_WorksCorrectly()
        {
            var lhs = new DiagonalMatrix<int>(2);
            lhs[0, 0] = 1;
            lhs[1, 1] = 2;

            var rhs = new SymmetricMatrix<int>(2);
            rhs[0, 0] = -100;
            rhs[0, 1] = 1;
            rhs[1, 0] = 22;
            rhs[1, 1] = 50;

            var result = new SymmetricMatrix<int>(2);
            result[0, 0] = -99;
            result[0, 1] = 1;
            result[1, 0] = 22;
            result[1, 1] = 52;

            var actual = lhs.Add(rhs);

            for (int i = 0; i < result.Size; i++)
            {
                for (int j = 0; j < result.Size; j++)
                {
                    Assert.AreEqual(result[i, j], actual[i, j]);
                }
            }
        }
    }
}
