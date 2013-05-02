using System.Collections;
using System.Windows.Forms;

namespace FIFAUltimateTeamBot
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        #region Fields
        /// <summary>
        /// Specifies the column to be sorted.
        /// </summary>
        private int _ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder _OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object.
        /// </summary>
        private CaseInsensitiveComparer _ObjectCompare;
        #endregion

        #region Constructors
        /// <summary>
        /// Class constructor. Initializes various elements.
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'.
            _ColumnToSort = 0;
            // Initialize the sort order to 'none'.
            _OrderOfSort = SortOrder.None;
            // Initialize the CaseInsensitiveComparer object.
            _ObjectCompare = new CaseInsensitiveComparer();
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared.</param>
        /// <param name="y">Second object to be compared.</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'.</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects.
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            //Check the column. Compare the two items.
            switch (_ColumnToSort)
            {
                case 2:
                    {
                        compareResult = _ObjectCompare.Compare(int.Parse(listviewX.SubItems[_ColumnToSort].Text), int.Parse(listviewY.SubItems[_ColumnToSort].Text));
                        break;
                    }
                default:
                    {
                        compareResult = _ObjectCompare.Compare(listviewX.SubItems[_ColumnToSort].Text, listviewY.SubItems[_ColumnToSort].Text);
                        break;
                    }
            }

            // Calculate correct return value based on object comparison.
            // If ascending sort is selected, return normal result of compare operation.
            // If descending sort is selected, return negative result of compare operation.
            // Otherwise return '0' to indicate they are equal.
            if (_OrderOfSort == SortOrder.Ascending) { return compareResult; }
            else if (_OrderOfSort == SortOrder.Descending) { return -compareResult; }
            else { return 0; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set { _ColumnToSort = value; }
            get { return _ColumnToSort; }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set { _OrderOfSort = value; }
            get { return _OrderOfSort; }
        }
        #endregion
    }
}