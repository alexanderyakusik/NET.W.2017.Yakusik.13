namespace Collections.UnitTests
{
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class BinarySearchTreeTests
    {
        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.PreorderTraversalTestCases))]
        public void PreorderTraversal_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<int> tree,
            IEnumerable<int> expected)
        {
            CollectionAssert.AreEqual(expected, tree.PreorderTraverse());
        }

        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.InorderTraversalTestCases))]
        public void InorderTraversal_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<int> tree,
            IEnumerable<int> expected)
        {
            CollectionAssert.AreEqual(expected, tree.InorderTraverse());
        }

        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.InorderTraversalStringTestCases))]
        public void InorderTraversalString_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<string> tree,
            IEnumerable<string> expected)
        {
            CollectionAssert.AreEqual(expected, tree.InorderTraverse());
        }

        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.InorderTraversalBookTestCases))]
        public void InorderTraversalBook_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<Book> tree,
            IEnumerable<Book> expected)
        {
            CollectionAssert.AreEqual(expected, tree.InorderTraverse());
        }

        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.InorderTraversalPointTestCases))]
        public void InorderTraversalPoint_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<Point> tree,
            IEnumerable<Point> expected)
        {
            CollectionAssert.AreEqual(expected, tree.InorderTraverse());
        }

        [Test, TestCaseSource(typeof(BinarySearchTreeTestsData), nameof(BinarySearchTreeTestsData.PostorderTraversalTestCases))]
        public void PostorderTraversal_CorrectTreeCreated_WorksCorrectly(
            BinarySearchTree<int> tree,
            IEnumerable<int> expected)
        {
            CollectionAssert.AreEqual(expected, tree.PostorderTraverse());
        }

        private class BinarySearchTreeTestsData
        {
            public static IEnumerable InorderTraversalTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<int>(new[] { 5, 2, 7, 3, 9, 0, 4, 8 }),
                        new[] { 0, 2, 3, 4, 5, 7, 8, 9 });
                    yield return new TestCaseData(
                        new BinarySearchTree<int>(new[] { 5, 2, 7, 3, 9, 0, 4, 8 }, Comparer<int>.Create((lhs, rhs) => rhs - lhs)),
                        new[] { 9, 8, 7, 5, 4, 3, 2, 0 });
                }
            }

            public static IEnumerable InorderTraversalStringTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<string>(new[] { "asd", "bsd", "adc" }),
                        new[] { "adc", "asd", "bsd" });
                    yield return new TestCaseData(
                        new BinarySearchTree<string>(new[] { "asd", "bsd", "adc" }, Comparer<string>.Create((lhs, rhs) => rhs.CompareTo(lhs))),
                        new[] { "bsd", "asd", "adc" });
                }
            }

            public static IEnumerable InorderTraversalBookTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<Book>(new[] { new Book("A", "B"), new Book("A", "A"), new Book("C", "D") }),
                        new[] { new Book("A", "A"), new Book("A", "B"), new Book("C", "D") });
                    yield return new TestCaseData(
                        new BinarySearchTree<Book>(
                            new[] { new Book("A", "B"), new Book("A", "A"), new Book("C", "D") }, 
                            Comparer<Book>.Create((lhs, rhs) => lhs.Title.CompareTo(rhs.Title))),
                        new[] { new Book("A", "A"), new Book("A", "B"), new Book("C", "D") });
                }
            }

            public static IEnumerable InorderTraversalPointTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<Point>(new[] { new Point(100, 23), new Point(100, -64), new Point(99, 33) },
                            Comparer<Point>.Create((lhs, rhs) => lhs.X - rhs.X == 0 ? lhs.Y - rhs.Y : lhs.X - rhs.X)),
                        new[] { new Point(99, 33), new Point(100, -64), new Point(100, 23) });
                }
            }

            public static IEnumerable PostorderTraversalTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<int>(new[] { 5, 2, 7, 3, 9, 0, 4, 8 }),
                        new[] { 0, 4, 3, 2, 8, 9, 7, 5 });
                }
            }

            public static IEnumerable PreorderTraversalTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new BinarySearchTree<int>(new[] { 5, 2, 7, 3, 9, 0, 4, 8 }),
                        new[] { 5, 2, 0, 3, 4, 7, 9, 8 });
                }
            }
        }
    }
}
