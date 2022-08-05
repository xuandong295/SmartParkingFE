using LiveCharts;
using LiveCharts.Wpf;
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

namespace Final.LoginPage.View
{
    /// <summary>
    /// Interaction logic for BasicColumn.xaml
    /// </summary>
    public partial class BasicColumn : UserControl
    {
        public BasicColumn()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Cars in",
                    Values = new ChartValues<double> { 54, 50, 39, 50, 87, 70, 90 }
                }
            };

            ////adding series will update and animate the chart automatically
            //SeriesCollection.Add(new ColumnSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42 }
            //});

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
