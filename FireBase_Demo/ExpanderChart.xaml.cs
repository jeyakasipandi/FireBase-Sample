using Firebase.Database.Offline;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireBase_Demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpanderChart : ContentPage
    {
        DateTimeAxisViewModel viewModel;
        public ExpanderChart(DateTimeAxisViewModel dateTimeAxisViewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel = dateTimeAxisViewModel;
        }
    }

    public class DateTimeAxisViewModel
    {
        public ObservableCollection<Production> DateTimeData { get; set; }

        public DateTime Minimum { get; set; } = new DateTime(2020, 5, 1);
        public DateTime Maximum { get; set; } = new DateTime(2020, 2, 1);
        public DateTimeAxisViewModel()
        {
            DateTimeData = new ObservableCollection<Production>();

            Random rand = new Random();
            double value = 100;
            DateTime date = new DateTime(2017, 1, 1);

            for (int i = 0; i < 365; i++)
            {
                if (rand.NextDouble() > 0.5)
                    value += rand.NextDouble();
                else
                    value -= rand.NextDouble();


                DateTimeData.Add(new Production { Growth = value, Date = date });
                date = date.AddDays(1);

            }
        }
    }

    public class Production
    {
        public double Growth
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }
    }

}