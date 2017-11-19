namespace Collections
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T>
    {
        #region Private fields

        private TreeNode<T> head = new TreeNode<T>();
        private IComparer<T> comparer;

        #endregion

        #region Ctors

        /// <summary>
        /// Instaniates new binary search tree using defalt comparer.
        /// </summary>
        public BinarySearchTree()
        {
            comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Instaniates new binary search tree using specified <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">Object used to compare elements in the tree.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/>is null.</exception>
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException($"{nameof(comparer)} cannot be null.");
        }

        /// <summary>
        /// Instaniates new binary search tree using defalt comparer and filling it with items from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Source collection to insert items from.</param>
        public BinarySearchTree(IEnumerable<T> collection) : this()
        {
            FillTree(collection);
        }

        /// <summary>
        /// Instaniates new binary search tree using specified <paramref name="comparer"/> and filling it with items from <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection">Source collection to insert items from.</param>
        /// <param name="comparer">Object used to compare elements in the tree.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/>is null.</exception>
        public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer)
        {
            FillTree(collection);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds <paramref name="element"/> to the binary search tree.
        /// </summary>
        /// <param name="element">Added element.</param>
        /// <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} cannot be null.");
            }

            AddToNode(ref head.leftNode, element);
        }

        /// <summary>
        /// Performs a preorder tree traversal.
        /// </summary>
        /// <returns>Items enumeration.</returns>
        public IEnumerable<T> PreorderTraverse()
        {
            foreach (var item in Traverse(head.leftNode))
            {
                yield return item;
            }

            IEnumerable<T> Traverse(TreeNode<T> node)
            {
                if (node != null)
                {
                    yield return node.element;

                    foreach (var item in Traverse(node.leftNode))
                    {
                        yield return item;
                    }

                    foreach (var item in Traverse(node.rightNode))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Performs an inorder tree traversal.
        /// </summary>
        /// <returns>Items enumeration.</returns>
        public IEnumerable<T> InorderTraverse()
        {
            foreach (var item in Traverse(head.leftNode))
            {
                yield return item;
            }

            IEnumerable<T> Traverse(TreeNode<T> node)
            {
                if (node != null)
                {
                    foreach (var item in Traverse(node.leftNode))
                    {
                        yield return item;
                    }

                    yield return node.element;

                    foreach (var item in Traverse(node.rightNode))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Performs a postorder tree traversal.
        /// </summary>
        /// <returns>Items enumeration.</returns>
        public IEnumerable<T> PostorderTraverse()
        {
            foreach (var item in Traverse(head.leftNode))
            {
                yield return item;
            }

            IEnumerable<T> Traverse(TreeNode<T> node)
            {
                if (node != null)
                {
                    foreach (var item in Traverse(node.leftNode))
                    {
                        yield return item;
                    }

                    foreach (var item in Traverse(node.rightNode))
                    {
                        yield return item;
                    }

                    yield return node.element;
                }
            }
        }

        #endregion

        #region Private methods

        private void AddToNode(ref TreeNode<T> node, T element)
        {
            if (node == null)
            {
                node = new TreeNode<T>()
                {
                    element = element
                };

                return;
            }

            if (comparer.Compare(element, node.element) < 0)
            {
                AddToNode(ref node.leftNode, element);
            }
            else
            {
                AddToNode(ref node.rightNode, element);
            }
        }

        private void FillTree(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        #endregion

        #region TreeNode

        private class TreeNode<T>
        {
            public T element;
            public TreeNode<T> leftNode, rightNode;
        }

        #endregion
    }
}
