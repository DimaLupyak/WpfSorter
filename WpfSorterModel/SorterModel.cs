using Sorter.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSorter.Model
{
    public enum SorterState
    {
        Runing, Suspended, Unstarted, IsSorted
    }
    public enum SwapElementsMode
    {
        Swaping, Selecting
    }
    public class SorterModel : INotifyPropertyChanged
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

        public string name;
        private ASorter<int> sorter;
        private int[][] array;
        private bool arrayIsLoaded = false;
        private Thread sorterThread;
        private int sleepTime = 500;
        private SorterState state = SorterState.Unstarted;
        private int rowCount;
        private int columnCount;
        private SwapElementsMode swapMode = SwapElementsMode.Selecting;
        public ObservableCollection<ElementModel> Elements { get; private set; }
        #endregion

        #region Properties
        
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");

            }
        }
        public int[][] Array
        {
            private get { return array; }
            set
            {
                if (value != null)
                {
                    try
                    {                        
                        sorterThread.Abort();
                    }
                    catch (Exception) 
                    { 
                    }
                    swapMode = SwapElementsMode.Selecting;
                    array = value;
                    columnCount = 0;
                    RowCount = array.Length;                    

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Elements.Clear();
                    });

                    for (int i = 0; i < array.Length; i++)
                    {
                        for (int j = 0; j < array[i].Length; j++)
                        {
                            if (ColumnCount < array[i].Length)
                            {
                                ColumnCount = array[i].Length;
                            }
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                Elements.Add(new ElementModel(array[i][j], i, j));
                            });

                        }
                    }
                    
                    ArrayIsLoaded = true;
                    
                    sorterThread = new Thread(SortArray);
                    sorterThread.Name = sorter.GetType().Name;
                    State = SorterState.Unstarted;
                    
                }
            }
        }
        public int SleepTime
        {
            get
            {
                return sleepTime;
            }
            set
            {
                sleepTime = value;
                OnPropertyChanged("SleepTime");
            }
        }
        public SorterState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if (state == SorterState.IsSorted)
                {
                    foreach (var item in Elements.Where(x => x.IsSelected == true))
                    {
                        item.IsSelected = false;
                    }
                }
                OnPropertyChanged("State");
            }
        }
        public bool ArrayIsLoaded
        {
            get
            {
                return arrayIsLoaded;
            }
            private set
            {
                arrayIsLoaded = value;
                OnPropertyChanged("ArrayIsLoaded");

            }
        }
        public int RowCount
        {
            get { return rowCount; }
            private set
            {
                rowCount = value;
                OnPropertyChanged("RowCount");

            }
        }
        public int ColumnCount
        {
            get { return columnCount; }
            private set
            {
                columnCount = value;
                OnPropertyChanged("ColumnCount");

            }
        }
        #endregion

        #region Constructors and Initialization Methods

        public SorterModel(ASorter<int> sorter, string name = null, int[][] array = null)
        {
            Elements = new ObservableCollection<ElementModel>();
            this.sorter = sorter;
            Name = name;
            sorter.SwapElements += sorter_SwapElements;
            Array = array;
            sorterThread = new Thread(SortArray);
            sorterThread.Name = sorter.GetType().Name;
        }

        #endregion
        #region Public Methods
        public void ChangeSorterState()
        {
            if (State != SorterState.IsSorted)
            {
                switch (State)
                {
                    case SorterState.Unstarted:
                        sorterThread.IsBackground = true;
                        sorterThread.Start();
                        State = SorterState.Runing;
                        break;
                    case SorterState.Runing:
                        sorterThread.Suspend();
                        State = SorterState.Suspended;
                        break;
                    case SorterState.Suspended:
                        sorterThread.Resume();
                        State = SorterState.Runing;
                        break;
                }
            }
        }

        public void AbortSort()
        {
            try
            {
                sorterThread.Abort();
            }
            catch (Exception)
            { }
        }
        #endregion
        #region Sorter Event Hending Methods
        void sorter_SwapElements(object sender, EventArgs e)
        {
            if (e is SwapElementsArgs)
            {
                SwapElementsArgs args = (SwapElementsArgs)e;
                Thread.Sleep(SleepTime);
                if (swapMode == SwapElementsMode.Selecting)
                {
                    try
                    {
                        foreach (var item in Elements.Where(x => x.IsSelected == true))
                        {
                            item.IsSelected = false;
                        }
                    }
                    catch (Exception) { }
                    ElementModel element = null;
                    try
                    {
                        element = Elements.SingleOrDefault
                                (
                                    x =>
                                    x.ColumnNumber == args.FirstSwapElement.Y &&
                                    x.RowNumber == args.FirstSwapElement.X &&
                                    x.Value.ToString() == args.FirstSwapElement.Value &&
                                    x.IsSelected == false
                                );
                    }
                    catch (Exception) { }
                    if (element != null)
                    {
                        element.IsSelected = true;
                    }

                    try
                    {
                        element = Elements.SingleOrDefault
                                       (
                                       x =>
                                       x.ColumnNumber == args.SecondSwapElement.Y &&
                                       x.RowNumber == args.SecondSwapElement.X &&
                                       x.Value.ToString() == args.SecondSwapElement.Value &&
                                       x.IsSelected == false
                                       );
                    }
                    catch (Exception) { }
                    if (element != null)
                    {
                        element.IsSelected = true;
                        
                    }
                    swapMode = SwapElementsMode.Swaping;
                    return;
                }
                else if (swapMode == SwapElementsMode.Swaping)
                {
                    ElementModel element = null;
                    if (args.FirstSwapElement.Value != args.SecondSwapElement.Value)
                    {
                        try
                        {
                            element = Elements.SingleOrDefault
                                           (
                                           x =>
                                           x.ColumnNumber == args.FirstSwapElement.Y &&
                                           x.RowNumber == args.FirstSwapElement.X &&
                                           x.Value.ToString() == args.SecondSwapElement.Value &&
                                           x.IsSelected == true
                                           );
                        }
                        catch (Exception) { }

                        if (element != null)
                        {
                            element.ColumnNumber = args.SecondSwapElement.Y;
                        }
                        try
                        {
                            element = Elements.SingleOrDefault
                                           (
                                           x =>
                                           x.ColumnNumber == args.SecondSwapElement.Y &&
                                           x.RowNumber == args.SecondSwapElement.X &&
                                           x.Value.ToString() == args.FirstSwapElement.Value &&
                                           x.IsSelected == true
                                           );
                        }
                        catch (Exception)
                        { }
                        if (element != null)
                        {
                            element.ColumnNumber = args.FirstSwapElement.Y;
                        }
                    }
                    swapMode = SwapElementsMode.Selecting;
                    return;
                }
            }
        }
        #endregion
        #region Private Methods

        private void SortArray()
        {
            sorter.Sort(array);
            State = SorterState.IsSorted;

        }
        #endregion

    }
}
