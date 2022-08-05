using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
namespace Final.LoginPage.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for StackedLine.xaml
    /// </summary>
    public partial class StackedArea : UserControl
    {
        public StackedArea()
        {
            InitializeComponent();
 
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(10),
                        new ObservableValue(5),
                        new ObservableValue(2),
                        new ObservableValue(7),
                        new ObservableValue(7),
                        new ObservableValue(4)
                    },
                    PointGeometrySize = 0,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent
                }
            };
 
            DataContext = this;
        }
 
        public SeriesCollection SeriesCollection { get; set; }
 
      
    }
}