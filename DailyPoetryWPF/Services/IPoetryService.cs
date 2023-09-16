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
    }
}
