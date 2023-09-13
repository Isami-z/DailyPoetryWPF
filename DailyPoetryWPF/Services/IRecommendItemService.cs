using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public interface IRecommendItemService
    {
        Task<RecommendItem> GetRecommendContentAsync();
    }

    public class RecommendItem
    {
        public string id { get; set; }
        public string content { get; set; }
        public int popularity { get; set; }
        public Origin origin { get; set; }
        public List<string> matchTags { get; set; }
        public string recommendedReason { get; set; }
        public DateTime cacheAt { get; set; }
    }

    public class Origin
    {
        public string title { get; set; }
        public string dynasty { get; set; }
        public string author { get; set; }
        public List<string> content { get; set; }
        public List<string> translate { get; set; }
    }

    public class Recommend
    {
        public string status { get; set; }
        public RecommendItem data { get; set; }
        public string token { get; set; }
        public string ipAddress { get; set; }
        public object warning { get; set; }
    }

}
