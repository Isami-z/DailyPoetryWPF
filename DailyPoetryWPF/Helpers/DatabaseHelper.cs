using DailyPoetryWPF.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Helpers
{
    public class DatabaseHelper
    {
        private static SQLiteAsyncConnection conn;
        public static SQLiteAsyncConnection ConnectDatabase()
        {
            if (conn == null)
            {
                string baseDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string databaseName = "db.sqlite3";

                if (!File.Exists(Path.Combine(baseDataDirectory, databaseName)))
                {
                    ResourceHelper.ExtractResourceToFile(@"db.sqlite3", Path.Combine(baseDataDirectory, databaseName));
                }

                conn = new SQLiteAsyncConnection(Path.Combine(baseDataDirectory, databaseName));
                conn.CreateTableAsync<PreferenceStorage>();
            }
            return conn;
        }
    }
}
