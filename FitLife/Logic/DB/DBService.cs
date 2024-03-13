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
            //used for deleting db in debug, might add debug function later
            _connection.DeleteAllAsync<Weight>(); 
            _connection.DeleteAllAsync<Macro>();


            _connection.CreateTableAsync<Weight>();
            _connection.CreateTableAsync<Macro>();
        }

        //FOR WEIGHT:

        public async Task<List<Weight>> GetWeekWeight(DateTime date) //ger en lista på vikter under vikten in-datumet ligger i baserat på dagen datum. funkar bara på dagens datum
        {
            DateTime monday = DateTime.Today.AddDays(ConstantsDB.daysToMonday);
            DateTime sunday = DateTime.Today.AddDays(ConstantsDB.daysToSunday);

            Debug.WriteLine("getting this weeks weight");
            return await _connection.Table<Weight>()
                                .Where(d => d.Date >= monday && d.Date <= sunday)
                                .OrderBy(d => d.Date)
                                .ToListAsync();
        }

        public async Task<List<Weight>> GetAllTimeWeight()
        {
            return await _connection.Table<Weight>().OrderBy(d => d.Date).ToListAsync();
        }

        public async Task CreateWeight(Weight newWeight)
        {
            Weight oldWeight = await _connection.Table<Weight>()
                .Where(d => d.Date == newWeight.Date)
                .FirstOrDefaultAsync();

            if (oldWeight == null)
            {
                await _connection.InsertAsync(newWeight);
                Debug.WriteLine("Adding new weight for date: " + newWeight.Date.ToString());
            } else { Debug.WriteLine("weight already inputted for date: " + newWeight.Date.ToString()); }
        } 


        //FOR MACROS:

        public async Task CreateMacro(Macro newMacro)
        {
            var oldMacro = await _connection.Table<Macro>()
                .Where(d => d.Date == newMacro.Date)
                .FirstOrDefaultAsync();

            if (oldMacro == null)
            {
                await _connection.InsertAsync(newMacro);
                Debug.WriteLine("adding new macro for date: " + newMacro.Date.ToString());
            }
            else
            {
                newMacro.Protein += oldMacro.Protein;
                newMacro.Kcal += oldMacro.Kcal;
                await _connection.UpdateAsync(newMacro);
                Debug.WriteLine("updating macro for date: " + newMacro.Date.ToString());
            }
        }

        public async Task<List<Macro>> GetWeekMacro(DateTime date) //funkar bara på dagens datum
        {
            DateTime monday = DateTime.Today.AddDays(ConstantsDB.daysToMonday);
            DateTime sunday = DateTime.Today.AddDays(ConstantsDB.daysToSunday);

            Debug.WriteLine("getting this weeks weight");
            return await _connection.Table<Macro>()
                                .Where(d => d.Date >= monday && d.Date <= sunday)
                                .OrderBy(d => d.Date)
                                .ToListAsync();
        }

        public async Task<List<Macro>> GetAllTimeMacro()
        {
            return await _connection.Table<Macro>().OrderBy(d => d.Date).ToListAsync();
        }
    }
}
