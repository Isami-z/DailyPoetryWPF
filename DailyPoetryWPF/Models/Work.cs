

using SQLite;
using System.Collections.Generic;

namespace DailyPoetryWPF.Models;
[Table("works")]
public class Work
{
    [PrimaryKey]
    [Column("id")]
    public long Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("rhesises_count")]
    public long? RhesisesCount { get; set; }
    [Column("categories_count")]
    public long? CategoriesCount { get; set; }
    [Column("author_name")]
    public string? AuthorName { get; set; }
    [Column("author_id")]
    public long? AuthorId { get; set; }
    [Column("dynasty")]
    public string? Dynasty { get; set; }
    [Column("type")]
    public string? Type { get; set; }
    [Column("foreward")]
    public string? Foreword { get; set; }
    [Column("content")]
    public string? Content { get; set; }
    [Column("intro")]
    public string? Intro { get; set; }
    [Column("annotation")]
    public string? Annotation { get; set; }
    [Column("translation")]
    public string? Translation { get; set; }
    [Column("appreciation")]
    public string? Appreciation { get; set; }
    [Column("famousReviews")]
    public string? FamousReviews { get; set; }
    [Column("layout")]
    public string? Layout { get; set; }
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public virtual ICollection<RecentView> RecentViews { get; set; } = new List<RecentView>();
}
