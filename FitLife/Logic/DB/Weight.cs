using SQLite;

namespace FitLife.Logic.DB
{
    [Table("daily_weight")]
    public class Weight
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("date"), Unique]
        public DateTime Date { get; set; }
        [Column("weight")]
        public float DailyWeight { get; set; }
    }
}
