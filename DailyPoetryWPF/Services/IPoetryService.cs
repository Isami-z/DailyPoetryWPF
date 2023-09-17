using DailyPoetryWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public interface IPoetryService
    {

        public Task<Author> GetAuthorById(int id);
        public Task<Work> GetWorkById(int id);
        public Task<List<Work>> GetWorkList();
        public void GetPoetryListByName(ref List<Work> works, string value);
        public void GetPoetryListByContent(ref List<Work> works, string value);
        public void GetPoetryListByAuthor(ref List<Work> works, string value);

        public Task<Favorite> IsFavorite(int id);
        public Task InsertFavorite(Favorite favorite);
        public Task DeleteFavorite(Favorite favorite);
        public Task<List<Work>> GetFavoritePoetry();

        public Task<RecentView> IsRecent(int id);
        public Task InsertRecent(RecentView recent);
        public Task DeleteRecent(RecentView recent);
        public Task<List<Work>> GetRecentPoetry();
    }
}
