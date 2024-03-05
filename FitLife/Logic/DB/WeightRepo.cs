using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FitLife.Logic.DB;

public class WeightRepo
{
    SQLiteAsyncConnection Database;
    public WeightRepo()
    {
    }
    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(ConstantsDB.DatabasePath, ConstantsDB.Flags);
        var result = await Database.CreateTableAsync<Weight>();
    }

    public async Task<List<Weight>> GetWeightAsync()
    {
        await Init();
        return await Database.Table<Weight>().ToListAsync();
    }

    public async Task<Weight> GetWeightAsync(DateOnly date)
    {
        await Init();
        return await Database.Table<Weight>().Where(i => i.Date == date).FirstOrDefaultAsync();
    }

    public async Task<int> SaveWeightAsync(int weight)
    {
        await Init();
        return await Database.InsertAsync(new Weight { DailyWeight = weight, Date = DateOnly.FromDateTime(DateTime.Today)});
    }


    //läs på lite mer om denna
    public async Task<int> DeleteWeightAsync(Weight item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}