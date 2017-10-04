using ConwaysGameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConwaysGameOfLife.UI.Converters
{
    public class GenerationToRowsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = value as IGeneration;
            if (array == null) return null;
            var rows = array.Rows;
            if (rows == 0) return null;
            var columns = array.Columns;
            if (columns == 0) return null;
            return array.GetRows();
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
