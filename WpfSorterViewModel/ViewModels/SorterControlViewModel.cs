using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfSorter.Model;

namespace WpfSorter.ViewModel
{
    public class SorterControlViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public SorterControlViewModel(SorterModel sorter)
        {
            ChangeStateClickCommand = new Command(arg => ChangeStateClickMethod());
            MinimizeClickCommand = new Command(arg => MinimizeClickMethod());
            Sorter = sorter;
        }      

        #endregion

        private Visibility dataVisibility = Visibility.Visible;

        #region Properties

        public SorterModel Sorter { get; set; }
        public Visibility DataVisibility
        {
            get
            {
                return dataVisibility;
            }
            set
            {
                dataVisibility = value;
                OnPropertyChanged("DataVisibility");
            }
        }
        #endregion


        #region Commands

        /// <summary>
        /// Get or set ClickCommand.
        /// </summary>
        public ICommand ChangeStateClickCommand { get; set; }
        public ICommand MinimizeClickCommand { get; set; }
        #endregion


        #region Methods

        /// <summary>
        /// Click method.
        /// </summary>
        private void ChangeStateClickMethod()
        {
            Sorter.ChangeSorterState();
        }
        private void MinimizeClickMethod()
        {
            if (DataVisibility == Visibility.Visible)
                DataVisibility = Visibility.Collapsed;
            else DataVisibility = Visibility.Visible;
        }
        #endregion

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
    }
}
