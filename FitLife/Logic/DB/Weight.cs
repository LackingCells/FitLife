using SQLite;

namespace FitLife.Logic.DB
{
    [Table("daily_weight")]
    public class Weight
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public DateTime Date { get; set; } //den klarar inte dateonly-formatet, hitta alternativ.
        [Column("weight")]
        public float DailyWeight { get; set; }
    }
}
