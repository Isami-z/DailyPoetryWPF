

using SQLite;

namespace DailyPoetryWPF.Models
{
    [Table("preferencestorage")]
    public class PreferenceStorage
    {
        [Column("key")]
        public string Key { get; set; }
        [Column("value")]
        public string Value { get; set; }
    }
}
