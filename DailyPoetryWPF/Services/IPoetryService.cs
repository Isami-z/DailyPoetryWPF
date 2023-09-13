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
    }
}
