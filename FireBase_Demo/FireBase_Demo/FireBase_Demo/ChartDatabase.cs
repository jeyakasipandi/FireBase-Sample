using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;

namespace FireBase_Demo
{
    public class ChartDatabase
    {
        readonly SQLiteConnection _database;

        public ChartDatabase(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<ChartModel>();
        }

        //Get the list of ChartDataModel items from the database
        public List<ChartModel> GetChartDataModel()
        {
            return _database.Table<ChartModel>().ToList();
        }

        //Insert an item in the database
        public int SaveChartDataModelAsync(ChartModel chartDataModel)
        {
            if (chartDataModel == null)
            {
                throw new Exception("Null");
            }

            return _database.Insert(chartDataModel);
        }

        //Delete an item in the database 
        public int DeleteChartDataModelAsync(ChartModel chartDataModel)
        {
            return _database.Delete(chartDataModel);
        }

        public void ClearChartDataModelAsync()
        {
            _database.DeleteAll<ChartModel>();
        }
    }
}
