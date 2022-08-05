using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace Final.LoginPage.View
{
    /// <summary>
    /// Interaction logic for CloudView.xaml
    /// </summary>
    public partial class CloudView : UserControl
    {
     
        public CloudView()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            SeriesCollection = new SeriesCollection() { 
                new PieSeries
                {
                    Title = "Day",
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(32),
                    },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Night",
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(41),
                    },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Evening",
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(21),
                    },
                    DataLabels = true
                }
            };
            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection SeriesCollection { get; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
