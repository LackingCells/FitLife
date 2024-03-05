using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitLife.Logic.DB
{
    [Table("daily_calories")]
    public class Calories
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Kcal {  get; set; }
        public int Protein { get; set; }
    }
}
