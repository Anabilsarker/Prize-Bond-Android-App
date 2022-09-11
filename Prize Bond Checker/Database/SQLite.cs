using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Prize_Bond_Checker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prize_Bond_Checker.Database
{
    public static class SQLite
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            string dbPath = System.IO.Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "prizebondDB.db3");
            Console.WriteLine("dataDir: " + dbPath);
            db = new SQLiteAsyncConnection(dbPath);
            await db.CreateTableAsync<Bonds>();
        }
        public static async Task AddBonds(string bondnumber)
        {
            await Init();
            Bonds bonds = new Bonds
            {
                BondNumber = bondnumber,
            };
            var id = await db.InsertAsync(bonds);
        }
    }
}