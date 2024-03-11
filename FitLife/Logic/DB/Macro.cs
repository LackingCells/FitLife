using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitLife.Logic.DB
{
    [Table("daily_calories")]
    public class Macro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("calories")]
        public int Kcal {  get; set; }
        [Column("protein")]
        public int Protein { get; set; }
        [Column("date"), Unique]
        public DateTime Date { get; set; }
    }
}
