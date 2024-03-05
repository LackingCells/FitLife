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
        [PrimaryKey]
        public DateOnly Date { get; set; }
        public int DailyWeight { get; set; }
    }
}
