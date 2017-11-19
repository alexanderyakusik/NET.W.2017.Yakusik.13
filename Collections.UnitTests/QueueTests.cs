namespace Collections.UnitTests
{
    using System;
    using System.Collections;
    using NUnit.Framework;

    [TestFixture]
    public class QueueTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Ctor_InvalidCapacityPassed_ArgumentOutOfRangeExceptionThrown(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Queue<int>(capacity));
        }
        
        [Test]
        public void Ctor_NullCollectionPassed_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new Queue<int>(null));
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.CtorEnumerableTestCases))]
        public void Ctor_CorrectCollectionPassed_WorksCorrectly(System.Collections.Generic.IEnumerable<object> enumerable, Queue<object> resultQueue)
        {
            var queue = new Queue<object>(enumerable);

            CollectionAssert.AreEqual(resultQueue, queue);
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.EnqueueTestCases))]
        public void Enqueue_CorrectValuesPassed_WorksCorrectly(Queue<int> sourceQueue, Queue<int> resultQueue, int[] objects)
        {
            foreach (var o in objects)
            {
                sourceQueue.Enqueue(o);
            }

            CollectionAssert.AreEqual(resultQueue, sourceQueue);
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.EnqueueWithDequeuingTestCases))]
        public void Enqueue_CorrectValuesPassed_WorksCorrectlyWithDequeuing(
            Queue<int> sourceQueue, 
            Queue<int> resultQueue, 
            int[] firstObjects, 
            int objectsToDequeue, 
            int[] secondObjects)
        {
            foreach (var o in firstObjects)
            {
                sourceQueue.Enqueue(o);
            }

            for (var i = 0; i < objectsToDequeue; i++)
            {
                sourceQueue.Dequeue();
            }

            foreach (var o in secondObjects)
            {
                sourceQueue.Enqueue(o);
            }

            CollectionAssert.AreEqual(resultQueue, sourceQueue);
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.EnqueueNullArgumentTestsData))]
        public void Enqueue_NullArgumentPassed_ArgumentNullExceptionThrown(Queue<object> sourceQueue, object item)
        {
            Assert.Throws<ArgumentNullException>(() => sourceQueue.Enqueue(item));
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.DequeueEmptyTestsData))]
        public void Dequeue_EmptyQueue_InvalidOperationExceptionThrown(Queue<object> sourceQueue)
        {
            Assert.Throws<InvalidOperationException>(() => sourceQueue.Dequeue());
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.DequeueTestCases))]
        public void Dequeue_CorrectValuesPassed_WorksCorrectly(Queue<int> sourceQueue, Queue<int> resultQueue, int amountToDequeue)
        {
            for (var i = 0; i < amountToDequeue; i++)
            {
                sourceQueue.Dequeue();
            }

            CollectionAssert.AreEqual(resultQueue, sourceQueue);
        }

        [Test, TestCaseSource(typeof(QueueTestsTestsData), nameof(QueueTestsTestsData.GetEnumeratorTestsData))]
        public void GetEnumerator_CollectionChangedDuringEnumeration_InvalidOperationThrown(Queue<int> initialQueue)
        {
            var enumerator = initialQueue.GetEnumerator();
            enumerator.MoveNext();
            initialQueue.Enqueue(1);
            Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        }

        private class QueueTestsTestsData
        {
            public static IEnumerable CtorEnumerableTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new object[] { 1, 2, 3, 4, 5 },
                        new Queue<object>(new object[] { 1, 2, 3, 4, 5 }));
                    yield return new TestCaseData(
                        new object[] { 1, 2, 3, 4 },
                        new Queue<object>(new object[] { 1, 2, 3, 4 }));
                }
            }

            public static IEnumerable EnqueueTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(new[] { 1, 2, 3, 4, 5, 6 }),
                        new[] { 1, 2, 3, 4, 5, 6 });
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(new[] { 0, 0, 0, 0, 0 }),
                        new[] { 0, 0, 0, 0, 0 });
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(),
                        new int[0]);
                }
            }

            public static IEnumerable EnqueueWithDequeuingTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(new[] { 3, 4, 5, 6, 7, 8 }),
                        new[] { 1, 2, 3 },
                        2,
                        new[] { 4, 5, 6, 7, 8 });
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(new[] { 6, 7, 8, 9, 10, 11, 12, 13 }),
                        new[] { 1, 2, 3, 4, 5 },
                        5,
                        new[] { 6, 7, 8, 9, 10, 11, 12, 13 });
                    yield return new TestCaseData(
                        new Queue<int>(),
                        new Queue<int>(new[] { 2, 3, 4, 5, 6, 7 }),
                        new[] { 1, 2, 3, 4, 5 },
                        1,
                        new[] { 6, 7 });
                }
            }

            public static IEnumerable EnqueueNullArgumentTestsData
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<object>(new object[] { 1, 2, 3, 4, 5 }),
                        null);
                }
            }

            public static IEnumerable DequeueTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<int>(new[] { 1, 2, 3, 4, 5 }),
                        new Queue<int>(new[] { 4, 5 }),
                        3);
                    yield return new TestCaseData(
                        new Queue<int>(new[] { 1, 2, 3, 4, 5, 6, 7 }),
                        new Queue<int>(new[] { 7 }),
                        6);
                    yield return new TestCaseData(
                        new Queue<int>(new[] { 1, 2, 3, 4, 5 }),
                        new Queue<int>(new int[0]),
                        5);
                }
            }

            public static IEnumerable DequeueEmptyTestsData
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<object>());
                }
            }

            public static IEnumerable GetEnumeratorTestsData
            {
                get
                {
                    yield return new TestCaseData(
                        new Queue<int>(new[] { 1, 2, 3 }));
                }
            }
        }
    }
}
