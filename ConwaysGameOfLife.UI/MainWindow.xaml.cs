using ConwaysGameOfLife.Logic;
using ConwaysGameOfLife.UI.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConwaysGameOfLife.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UniverseViewModel _universeViewModel;
        public MainWindow()
        {
            InitializeComponent();
          
            
            //3 background worker
            //4 border
            //5 here->
            //6 tests
            //7 slow cell update
            _universeViewModel = new UniverseViewModel(30,30,200);//TODO rows, columns
            DataContext = _universeViewModel;
        }

    }
}
