using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FitLife.Logic.DB
{
    [Table("daily_weight")]
    public class Weight
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public float DailyWeight { get; set; }
    }
}
