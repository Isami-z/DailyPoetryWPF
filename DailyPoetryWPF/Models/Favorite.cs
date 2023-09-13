

using SQLite;

namespace DailyPoetryWPF.Models;

[Table("favorite")]
public  class Favorite
{
    [AutoIncrement]
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }
    [Column("PoetryId")]
    public long? PoetryId { get; set; }

    public virtual Work? Poetry { get; set; }
}
