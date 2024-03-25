using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitLife.Logic.DB
{
    public class DBService
    {
        private const string dbName = "fitnessDB.db3";
        private readonly SQLiteAsyncConnection _connection;

        public DBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, dbName));
            _connection.CreateTableAsync<Weight>();
            _connection.CreateTableAsync<Macro>();
        }

        public async Task<Weight> getWeight(DateTime date)
        {
            return await _connection.Table<Weight>().Where(d => d.Date == date).FirstOrDefaultAsync();
        }

        public async Task<List<Weight>> GetAllTimeWeight()
        {
            return await _connection.Table<Weight>().OrderBy(d => d.Date).ToListAsync();
        }

        public async Task CreateWeight(Weight newWeight, Page page)
        {
            Weight oldWeight = await _connection.Table<Weight>()
                .Where(d => d.Date == newWeight.Date)
                .FirstOrDefaultAsync();
            string display;

            if (oldWeight == null)
            {
                await _connection.InsertAsync(newWeight);
                display = "Weight added successfully";
                Debug.WriteLine("Adding new weight for date: " + newWeight.Date.ToString());
            }
            else 
            { 
                Debug.WriteLine("weight already inputted for date: " + newWeight.Date.ToString()); 
                display = "Already entered todays weight"; 
            }

            await page.DisplayAlert("Weight added", display, "OK");
        }


        //FOR MACROS:

        public async Task CreateMacro(Macro newMacro, Page page)
        {
            var oldMacro = await _connection.Table<Macro>()
                .Where(d => d.Date == newMacro.Date)
                .FirstOrDefaultAsync();
            string display;

            if (oldMacro == null)
            {
                await _connection.InsertAsync(newMacro);
                display = "New macro entry created";
                Debug.WriteLine("adding new macro for date: " + newMacro.Date.ToString());
            }
            else
            {
                newMacro.Protein += oldMacro.Protein;
                newMacro.Kcal += oldMacro.Kcal;
                await _connection.UpdateAsync(newMacro);
                display = "Macro entry updated";
                Debug.WriteLine("updating macro for date: " + newMacro.Date.ToString());
            }
            await page.DisplayAlert("Macros updated", display, "OK");
        }

        //FLYTTA TILL WEIGHTPAGE
        //public async Task<List<Macro>> GetWeekMacro(DateTime date) 
        //{
        //    DateTime monday = getThisMonday(date);
        //    List<Macro> output = new List<Macro>();

        //    for (int i = 0; i < 7; i++)
        //    {
        //        output.Add(await _connection.Table<Macro>().Where(d => d.Date == monday.AddDays(i)).FirstOrDefaultAsync());
        //    }

        //    return output;
        //    Debug.WriteLine("getting this weeks macros");

        //    //Funkar inte
        //    //return await _connection.Table<Macro>()
        //    //                    .Where(d => d.Date >= monday && d.Date <= sunday)
        //    //                    .OrderBy(d => d.Date)
        //    //                    .ToListAsync();
        //}

        public async Task<List<Macro>> GetAllTimeMacro()
        {
            return await _connection.Table<Macro>().OrderBy(d => d.Date).ToListAsync();
        }
    }
}
