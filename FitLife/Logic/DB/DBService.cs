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
            _connection.CreateTableAsync<Weight>();
        }

        public async Task<List<Weight>> GetWeekWeight() //ta in dagens datum och använd days to monday och sunday med adddays för att ge alla vikter specifik vecka
        {
            return await _connection.Table<Weight>().ToListAsync();
        }

        public async Task Create(Weight newWeight)
        {
            await _connection.InsertAsync(newWeight);
        }

        //public async Task Update() för uppdatering av kalorier och protein, lägg ta existerande och uppdatera med påplussad
        //{
        //    await _connection.UpdateAsync()
        //  }

    }
}
