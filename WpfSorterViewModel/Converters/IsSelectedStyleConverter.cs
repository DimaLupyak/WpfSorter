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
    public class IsSelectedStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isSelected = (bool)value;
            if(isSelected)
            {
                switch (parameter.ToString())
                {
                    case "Background":
                        return (Application.Current.FindResource("SellectedCellBackgraundBrush") as Brush);
                    case "BorderBrush":
                        return (Application.Current.FindResource("SellectedCellBorderBrush") as Brush); 
                    default:
                        break;
                }
            }
            else
            {
                switch (parameter.ToString())
                {
                    case "Background":
                        return (Application.Current.FindResource("CellBackgraundBrush") as Brush);
                    case "BorderBrush":
                        return (Application.Current.FindResource("CellBorderBrush") as Brush);
                    default:
                        break;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
