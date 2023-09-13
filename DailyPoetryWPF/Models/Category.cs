using SQLite;

namespace DailyPoetryWPF.Models;

[Table("categories")]
public class Category
{
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }
    [Column("ordernumber")]
    public long? Ordernumber { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("slogan")]
    public string? Slogan { get; set; }
    [Column("intro")]
    public string? Intro { get; set; }
    [Column("library")]
    public string? Library { get; set; }
    [Column("press")]
    public string? Press { get; set; }
    [Column("works_count")]
    public long? WorksCount { get; set; }
    [Column("rhesises_count")]
    public long? RhesisesCount { get; set; }
}
