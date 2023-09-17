using SQLite;
namespace DailyPoetryWPF.Models;

[Table("recent_view")]
public  class RecentView
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("Id")]
    public long Id { get; set; }
    [Column("PoetryItemId")]
    public long PoetryItemId { get; set; }
}
