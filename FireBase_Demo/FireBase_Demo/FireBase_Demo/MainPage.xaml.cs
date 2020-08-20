using Firebase.Database;
using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireBase_Demo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            App.Database.ClearChartDataModelAsync();
            foreach(var item in viewModel.DataValues)
            {
                App.Database.SaveChartDataModelAsync(item);
            }

            paretoChart.Source = App.Database.GetChartDataModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpanderChart(new DateTimeAxisViewModel()));
        }
    }

    public class ChartModel
    {
        [PrimaryKey]
        public string XData { get; set; }
        public double YData { get; set; }
    }
    public class ViewModel
    {
        public ObservableCollection<ChartModel> DataValues { get; set; }

        public ViewModel()
        {
            DataValues = new ObservableCollection<ChartModel>()
            {
                new ChartModel(){XData = "Broken Links", YData = 349},
                new ChartModel(){XData = "Spelling Errors", YData = 169},
                new ChartModel(){XData = "Missing Title", YData = 79},
                new ChartModel(){XData = "Missing Description", YData = 77},
                new ChartModel(){XData = "Broken Image", YData = 45},
                new ChartModel(){XData = "Script Error", YData = 30},
                new ChartModel(){XData = "Incorrect Use of Headings", YData = 15},
                new ChartModel(){XData = "Missing ALT tags", YData = 14},
                new ChartModel(){XData = "Browser Compatibility", YData = 12},
                new ChartModel(){XData = "Security Warning", YData = 9},
            };
        }
    }
}

