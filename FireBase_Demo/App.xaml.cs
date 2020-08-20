using Firebase.Database;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireBase_Demo
{
    public partial class App : Application
    {
        private static ChartDatabase database;

        public static ChartDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ChartDatabase(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                 "ChartDataBase.db3"));
                }

                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            var tabPage = new TabbedPage();
            tabPage.Children.Add(new Page() { Title = "1" });
            tabPage.Children.Add(new Page() { Title = "2" });
            tabPage.Children.Add(new Page() { Title = "3" });
            tabPage.Children.Add(new MainPage() { Title = "4" });
            tabPage.Children.Add(new ExpanderChart(new DateTimeAxisViewModel()) { Title = "chart" });
            MainPage = tabPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
