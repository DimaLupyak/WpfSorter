using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSorter.Model
{
    public class ElementModel : INotifyPropertyChanged
    {
        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        #region Fields

        private int value;
        private int rowNumber;
        private int columnNumber;
        private bool isSelected;
        #endregion

        #region Properties

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");

            }
        }
        public int RowNumber
        {
            get { return rowNumber; }
            set
            {
                this.rowNumber = value;
                OnPropertyChanged("RowNumber");

            }
        }
        public int ColumnNumber
        {
            get { return columnNumber; }
            set
            {
                this.columnNumber = value;
                OnPropertyChanged("ColumnNumber");

            }
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                this.isSelected = value;
                OnPropertyChanged("IsSelected");

            }
        }
        #endregion

        #region Constructors and Initialization Methods

        public ElementModel(int value, int rowNumber, int columnNumber)
        {
            Value = value;
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
            IsSelected = false;
        }

        #endregion
    }
}
