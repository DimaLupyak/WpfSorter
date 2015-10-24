using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using WpfSorter.Model;
using System.Linq;
using System.Threading;
using System.Text;
using Sorter.Base;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using DBMannager;

namespace WpfSorter.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Data

        public ObservableCollection<SorterControlViewModel> SorterControlViewModels { get; private set;}
        private bool arrayIsLoaded = false;
        private int sorterSleepTime = 500;
        public int SorterSleepTime
        {
            get
            {
                return sorterSleepTime;
            }
            set
            {
                sorterSleepTime = value;
                OnPropertyChanged("SorterSleepTime");
                foreach (var item in SorterControlViewModels)
                {
                    item.Sorter.SleepTime = sorterSleepTime;
                }
            }
        }
        public bool ArrayIsLoaded
        {
            get
            {
                return arrayIsLoaded;
            }
            set
            {
                arrayIsLoaded = value;
                OnPropertyChanged("ArrayIsLoaded");
            }
        }
        #endregion  

        #region Constructor

        public MainViewModel()
        {
            ChangeAllSortersStateCommand = new Command(arg => ChangeAllSortersState());
            LoadArrayFromFileCommand = new Command(arg => new Thread(LoadArrayFromFile).Start());
            LoadArrayFromDBCommand = new Command(arg => new Thread(LoadArrayFromDB).Start());
            CloseWindowCommand = new Command(arg => CloseWindow());
            SorterControlViewModels = new ObservableCollection<SorterControlViewModel>();
            LoadSorters();            
        }

        #endregion

        
        #region Commands

        public ICommand ChangeAllSortersStateCommand { get; set; }
        public ICommand LoadArrayFromFileCommand { get; set; }
        public ICommand LoadArrayFromDBCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        #region Logic

        public void CloseWindow() 
        {
            Application.Current.Shutdown();
        }

        public void LoadArrayFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (SorterControlViewModel sorterControlViewModel in SorterControlViewModels)
                {
                    sorterControlViewModel.Sorter.Array = ArrayManager.GetIntArrayFromFile(openFileDialog.FileName);
                }
            }
            ArrayIsLoaded = true;
            
        }

        public void LoadArrayFromDB()
        {
            DataBaseManager dataBaseMannager = DataBaseManager.Instance;
            foreach (SorterControlViewModel sorterControlViewModel in SorterControlViewModels)
            {
                sorterControlViewModel.Sorter.Array = dataBaseMannager.GetArrays();
            }
            ArrayIsLoaded = true;
        }

        private void ChangeAllSortersState()
        {
            foreach (SorterControlViewModel sorterControlViewModel in SorterControlViewModels)
            {
                sorterControlViewModel.Sorter.ChangeSorterState();
            }
        }

        private void SetSorterSleepTime(int time)
        {
            foreach (SorterControlViewModel sorterControlViewModel in SorterControlViewModels)
            {
                sorterControlViewModel.Sorter.SleepTime = time;
            }
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
        private void LoadSorters()
        {
            try
            {
                string[] dllFileNames = null;
                if (Directory.Exists(@"Plugins"))
                {
                    dllFileNames = Directory.GetFiles(@"Plugins", "*.dll");
                }
                foreach (string dllFile in dllFileNames)
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    var sorterTypes = from t in assembly.GetTypes()
                                      where (t.GetCustomAttribute(typeof(SorterAttribute)) != null)
                                      select t.MakeGenericType(new Type[] { typeof(int) });

                    foreach (Type sorterType in sorterTypes)
                    {
                        ASorter<int> aSorter = assembly.CreateInstance(sorterType.FullName, true) as ASorter<int>;
                        string sorterName = (sorterType.GetCustomAttribute(typeof(SorterAttribute)) as SorterAttribute).SorterName;
                        SorterModel sorter = new SorterModel(aSorter, sorterName);
                        SorterControlViewModels.Add(new SorterControlViewModel(sorter));
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
            } 
        }
    }
}