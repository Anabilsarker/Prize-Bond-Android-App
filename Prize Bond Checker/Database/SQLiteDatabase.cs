using Prize_Bond_Checker.Models;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Prize_Bond_Checker.Database
{
    public static class SQLiteDatabase
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "prizebondDB.db");
            Console.WriteLine("dataDir: " + dbPath);
            db = new SQLiteAsyncConnection(dbPath);
            await db.CreateTableAsync<Bonds>();
        }
        public static async Task AddBonds(long bondnumber)
        {
            await Init();
            Bonds bonds = new Bonds
            {
                BondNumber = bondnumber
            };
            await db.InsertAsync(bonds);
        }
        public static async Task DeleteBonds(long bondnumber)
        {
            Bonds bonds = new Bonds
            {
                BondNumber = bondnumber
            };
            await db.DeleteAsync(bonds);
        }
    }
}