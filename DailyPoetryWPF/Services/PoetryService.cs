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
    }
}
