using System.Windows;
using WpfSorter.ViewModel;

namespace WpfSorter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : ThemedWindow
    {
        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}