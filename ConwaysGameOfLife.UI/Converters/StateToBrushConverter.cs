using ConwaysGameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife.UI.Converters
{
    public class StateToBrushConverter : IValueConverter
    {
        public SolidColorBrush AliveColour { get; private set; }

        public SolidColorBrush DeadColour { get; private set; }
        public StateToBrushConverter(SolidColorBrush aliveColour, SolidColorBrush deadColour)
        {
            this.AliveColour = aliveColour;
            this.DeadColour = deadColour;
        }
        public StateToBrushConverter()
        {
            this.AliveColour = Brushes.Black;
            this.DeadColour = Brushes.White;
        }
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CellState state = (CellState)value;
            return state == CellState.Alive ? this.AliveColour: this.DeadColour;  
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SolidColorBrush)
                return ((SolidColorBrush)value) == this.AliveColour ? CellState.Alive : CellState.Dead;

            return CellState.Dead;
        }
    }

    
}
