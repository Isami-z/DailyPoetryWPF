using DailyPoetryWPF.Helpers;
using DailyPoetryWPF.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public class PoetryService : IPoetryService
    {
        private SQLiteAsyncConnection _connection;
        public PoetryService()
        {
            _connection = DatabaseHelper.ConnectDatabase();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var item = await _connection.FindAsync<Author>(id);
            return item;
        }

        public async Task<Work> GetWorkById(int id)
        {
            var item = await _connection.FindAsync<Work>(id);
            return item;
        }

        public async Task<List<Work>> GetWorkList()
        {
            var item = await _connection.Table<Work>().ToListAsync();
            return item;
        }

        public void GetPoetryListByAuthor(ref List<Work> works, string value)
        {
            works = works.Where(item => item.AuthorName.Contains(value)).ToList();
        }

        public void GetPoetryListByContent(ref List<Work> works, string value)
        {
            works = works.Where(item => item.Content.Contains(value)).ToList();
        }

        public void GetPoetryListByName(ref List<Work> works, string value)
        {
            works = works.Where(item => item.Name.Contains(value)).ToList();
        }

        public async Task<Favorite> IsFavorite(int id)
        {
            Favorite work = await _connection.FindAsync<Favorite>(t => t.PoetryId == id);
            return work;
        }

        public async Task InsertFavorite(Favorite favorite)
        {
            await _connection.InsertAsync(favorite);
        }

        public async Task DeleteFavorite(Favorite favorite)
        {
            var work = await IsFavorite((int)favorite.PoetryId);
            await _connection.DeleteAsync(work);
        }

        public async Task<List<Work>> GetFavoritePoetry()
        {
            var WorkItems = await _connection.Table<Work>().ToListAsync();
            var FavoriteItems = await _connection.Table<Favorite>().ToListAsync();
            var query = from a in WorkItems
                        join b in FavoriteItems
                        on a.Id equals b.PoetryId
                        select a;
            return query.ToList();
        }

        public async Task<RecentView> IsRecent(int id)
        {
            RecentView work = await _connection.FindAsync<RecentView>(t => t.PoetryItemId == id);
            return work;
        }

        public async Task InsertRecent(RecentView recent)
        {
            await _connection.InsertAsync(recent);
        }

        public async Task DeleteRecent(RecentView recent)
        {
            var work = await IsRecent((int)recent.PoetryItemId);
            await _connection.DeleteAsync(work);
        }

        public async Task<List<Work>> GetRecentPoetry()
        {
            var WorkItems = await _connection.Table<Work>().ToListAsync();
            var RecnetItems = await _connection.Table<RecentView>().ToListAsync();
            var query = from a in WorkItems
                        join b in RecnetItems
                        on a.Id equals b.PoetryItemId
                        select a;
            return query.ToList();
        }
    }
}
