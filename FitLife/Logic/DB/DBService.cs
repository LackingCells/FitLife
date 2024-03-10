using SQLite;
using System;
using System.Collections.Generic;
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
            //_connection.DeleteAllAsync<Weight>(); 
            //_connection.DeleteAllAsync<Calories>();
            _connection.CreateTableAsync<Weight>();
            _connection.CreateTableAsync<Macro>();
        }

        public async Task<List<Weight>> GetWeekWeight(DateTime date) //ger en lista på vikter under vikten in-datumet ligger i baserat på dagen datum. funkar bara på dagens datum
        {
            return await _connection.Table<Weight>()
                .Where(d => d.Date >= DateTime.Today.AddDays(ConstantsDB.daysToMonday) && d.Date <= DateTime.Today.AddDays(ConstantsDB.daysToSunday))
                .ToListAsync(); //dubbelchecka så datumen 100% är rätt
        }

        public async Task CreateWeight(Weight newWeight)
        {
            var oldWeight = await _connection.Table<Weight>()
                .Where(d => d.Date == DateTime.Today)
                .FirstOrDefaultAsync();

            if (oldWeight == null)
            {
                await _connection.InsertAsync(newWeight);
            }
        }

        public async Task CreateMacro(Macro newMacro)
        {
            var oldMacro = await _connection.Table<Macro>()
                .Where(d => d.Date == DateTime.Today)
                .FirstOrDefaultAsync();

            if (oldMacro == null)
            {
                await _connection.InsertAsync(newMacro);
            }
            else
            {
                newMacro.Protein += oldMacro.Protein;
                newMacro.Kcal += oldMacro.Kcal;
                await _connection.UpdateAsync(newMacro);
            }
        }

        //public async Task Update() för uppdatering av kalorier och protein, lägg ta existerande och uppdatera med påplussad
        //{
        //    await _connection.UpdateAsync()
        //  }

    }
}
