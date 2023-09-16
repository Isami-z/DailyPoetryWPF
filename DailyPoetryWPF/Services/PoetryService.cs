using DailyPoetryWPF.Helpers;
using DailyPoetryWPF.Models;
using SQLite;
using System;
using System.Collections.Generic;
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
    }
}
