

using SQLite;

namespace DailyPoetryWPF.Models
{
    [Table("preferencestorage")]
    public class PreferenceStorage
    {
        [PrimaryKey]
        [Column("key")]
        public string Key { get; set; }
        [Column("value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
