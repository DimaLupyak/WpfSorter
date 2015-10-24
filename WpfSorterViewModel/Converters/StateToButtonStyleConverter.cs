using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using WpfSorter.Model;

namespace WpfSorter.ViewModel.Converters
{
    public class StateToButtonStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SorterState state = (SorterState)value;
            if (parameter.ToString() == "Content")
                switch (state)
                {
                    case SorterState.Runing:
                        return "Suspend";
                    case SorterState.Suspended:
                        return "Resume";
                    case SorterState.Unstarted:
                        return "Start";
                    case SorterState.IsSorted:
                        return "Is Sorted";
                    default:
                        return DependencyProperty.UnsetValue;
                }
            if (parameter.ToString() == "Background")
            {
                try
                {
                    switch (state)
                    {
                        case SorterState.Runing:
                            return (Application.Current.FindResource("StopBrush") as Brush);
                        case SorterState.Suspended:
                            return (Application.Current.FindResource("StartBrush") as Brush);
                        case SorterState.Unstarted:
                            return (Application.Current.FindResource("ExpanderCaptionBrush") as Brush);
                        case SorterState.IsSorted:
                            return (Application.Current.FindResource("IsSortedBrush") as Brush);
                        default:
                            return DependencyProperty.UnsetValue;
                    }
                }
                catch (Exception)
                {                    
                    return DependencyProperty.UnsetValue;
                }
            }
            if (parameter.ToString() == "MouseOverBackground")
            {
                try
                {
                    switch (state)
                    {
                        case SorterState.Runing:
                            return (Application.Current.FindResource("SellectedStopBrush") as Brush);
                        case SorterState.Suspended:
                            return (Application.Current.FindResource("SellectedStartBrush") as Brush);
                        case SorterState.Unstarted:
                            return (Application.Current.FindResource("SellectedStartBrush") as Brush);
                        case SorterState.IsSorted:
                            return (Application.Current.FindResource("SellectedIsSortedBrush") as Brush);
                        default:
                            return DependencyProperty.UnsetValue;
                    }
                }
                catch (Exception)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            if (parameter.ToString() == "IsEnabled")
            {
                if (state == SorterState.IsSorted) return false;
                else return true;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
