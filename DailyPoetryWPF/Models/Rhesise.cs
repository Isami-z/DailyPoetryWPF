
using SQLite;

namespace DailyPoetryWPF.Models;

[Table("rhesises")]
public  class Rhesise
{
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }
    [Column("content")]
    public string? Content { get; set; }
    [Column("author_id")]
    public long? AuthorId { get; set; }
    [Column("author_name")]
    public string? AuthorName { get; set; }
    [Column("ordernumber")]
    public long? Ordernumber { get; set; }
    [Column("work_name")]
    public string? WorkName { get; set; }
    [Column("work_id")]
    public long? WorkId { get; set; }
    [Column("category_id")]
    public long? CategoryId { get; set; }
}
