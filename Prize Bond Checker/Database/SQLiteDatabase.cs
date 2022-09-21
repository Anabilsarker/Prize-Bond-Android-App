using Prize_Bond_Checker.Models;
using SQLite;
using System;
using System.Collections.Generic;
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
            db = new SQLiteAsyncConnection(dbPath);
            await db.CreateTableAsync<Bonds>();
        }
        public static async Task AddBonds(int bondnumber)
        {
            await Init();
            Bonds bonds = new Bonds
            {
                BondNumber = bondnumber
            };
            await db.InsertAsync(bonds);
        }
        public static async Task<List<Bonds>> GetBonds()
        {
            await Init();
            var list = await db.QueryAsync<Bonds>("select * from Bonds;");
            return list;
        }
        public static async Task<bool> BondExists(int bondnumber)
        {
            await Init();
            var count = await db.QueryAsync<Bonds>($"select * from Bonds WHERE BondNumber={bondnumber};");
            if (count.Count > 0)
                return true;
            else
                return false;
        }
        public static async Task DeleteBonds(int bondnumber)
        {
            Bonds bonds = new Bonds
            {
                BondNumber = bondnumber
            };
            await db.DeleteAsync(bonds);
        }
    }
}