

using SQLite;
using System.Runtime.CompilerServices;

namespace DailyPoetryWPF.Models;

[Table("category_works")]
public  class CategoryWork
{
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }

    [Column("ordernumber")]
    public long? Ordernumber { get; set; }

    [Column("category")]
    public string? Category { get; set; }
    [Column("author_id")]
    public long? AuthorId { get; set; }
    [Column("author_name")]
    public string? AuthorName { get; set; }
    [Column("work_id")]
    public long? WorkId { get; set; }
    [Column("work_name")]
    public string? WorkName { get; set; }
    [Column("work_dynasty")]
    public string? WorkDynasty { get; set; }
    [Column("work_type")]
    public string? WorkType { get; set; }
}
