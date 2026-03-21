using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CS_K_WPF.Converter
{
    public class NumberToColorNoBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = (double)value;
            if (number < 50 && number > 0)
            {
                return Color.FromRgb(255, 69, 58);
            }
            else if (50 <= number && number < 80)
            {
                return Color.FromRgb(255, 214, 100);
            }
            else if (80 <= number && number <= 100)
            {
                return Color.FromRgb(85, 177, 85);
            }
            else
            {
                return Color.FromRgb(30, 30, 30);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
