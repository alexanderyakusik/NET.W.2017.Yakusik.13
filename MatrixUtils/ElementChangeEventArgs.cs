namespace MatrixUtils
{
    using System;

    public class ElementChangeEventArgs : EventArgs
    {
        #region Ctors

        /// <summary>
        /// Instaniates event arguments with the specified <paramref name="rowIndex"/> and <paramref name="columnIndex"/>.
        /// </summary>
        /// <param name="rowIndex">Row index where the element was changed.</param>
        /// <param name="columnIndex">Column index where the element was changed.</param>
        public ElementChangeEventArgs(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Row index where the element was changed.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Column index where the element was changed.
        /// </summary>
        public int ColumnIndex { get; }

        #endregion
    }
}
