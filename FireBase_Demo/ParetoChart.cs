using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FireBase_Demo
{
    public class ParetoChart : SfChart
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create("Source", typeof(object), typeof(ParetoChart), null, BindingMode.Default, null, OnSourceChanged);

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is INotifyCollectionChanged)
                (newValue as INotifyCollectionChanged).CollectionChanged += ParetoChart_CollectionChanged;
            if (oldValue is INotifyCollectionChanged)
                (oldValue as INotifyCollectionChanged).CollectionChanged -= ParetoChart_CollectionChanged;
            (bindable as ParetoChart).GenerateSeries();
        }

        private static void ParetoChart_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            (sender as ParetoChart).GenerateSeries();
        }

        private void GenerateSeries()
        {
            this.SuspendSeriesNotification();
            CalculateCumulative();
            foreach (var series in Series)
                series.ItemsSource = ItemsSource;
            this.ResumeSeriesNotification();
        }

        private void CalculateCumulative()
        {
            var dataSource = Source as List<ChartModel>;
            if (dataSource != null && dataSource.Count > 0)
            {
                ItemsSource.Clear();
                var orderedList = dataSource.OrderByDescending(x => x.YData).ToList();
                double cumulativeCount = 0;
                foreach (var data in orderedList)
                {
                    cumulativeCount += data.YData;
                    ItemsSource.Add(new ParetoChartModel() { XValues = data.XData, YValues = data.YData, CumulativeCount = cumulativeCount });
                }

                if (cumulativeCount > 0)
                {
                    foreach (var data in itemsSource)
                    {
                        data.Cumulative = (data.CumulativeCount / cumulativeCount) * 100;
                    }
                }
            }

        }

        private ObservableCollection<ParetoChartModel> itemsSource;
        internal ObservableCollection<ParetoChartModel> ItemsSource
        {
            get { return itemsSource; }
            set
            {
                if (value != null)
                {
                    itemsSource = value;
                }
            }
        }

        NumericalAxis CumulativeAxis { get; set; }
        ColumnSeries columnSeries;
        LineSeries cumulativeLineSeries;


        public ParetoChart() : base()
        {
            this.PrimaryAxis = new CategoryAxis() { LabelRotationAngle = 315 };
            this.SecondaryAxis = new NumericalAxis() { Interval = 50 };
            ItemsSource = new ObservableCollection<ParetoChartModel>();
            CumulativeAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, Interval = 10, PlotOffset = 10, OpposedPosition = true, ShowMajorGridLines = false, ShowMinorGridLines = false };
            CumulativeAxis.LabelStyle = new ChartAxisLabelStyle() { LabelFormat = "0.0 '%'" };
            columnSeries = new ColumnSeries() { XBindingPath = "XValues", YBindingPath = "YValues" };
            cumulativeLineSeries = new LineSeries() { XBindingPath = "XValues", YBindingPath = "Cumulative", YAxis = CumulativeAxis };
            cumulativeLineSeries.DataMarker = new ChartDataMarker() { ShowLabel = false, ShowMarker = true, MarkerType = DataMarkerType.Square, MarkerColor = Color.Black, MarkerHeight = 5, MarkerWidth = 5 };
            Series.Add(columnSeries);
            Series.Add(cumulativeLineSeries);
        }
    }
}
