using SQLite;
namespace DailyPoetryWPF.Models;

[Table("authors")]
public class Author
{
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("intro")]
    public string? Intro { get; set; }
    [Column("rhesises_count")]
    public long? RhesisesCount { get; set; }
    [Column("dynasty")]
    public string? Dynasty { get; set; }
    [Column("yob")]
    public long? Yob { get; set; }
    [Column("yod")]
    public long? Yod { get; set; }
    [Column("works_count")]
    public long? WorksCount { get; set; }
    [Column("shi_count")]
    public long? ShiCount { get; set; }
    [Column("ci_count")]
    public long? CiCount { get; set; }
    [Column("qu_count")]
    public long? QuCount { get; set; }
    [Column("fu_count")]
    public long? FuCount { get; set; }
}
