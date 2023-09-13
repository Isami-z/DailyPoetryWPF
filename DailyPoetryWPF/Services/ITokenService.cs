using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public interface ITokenService
    {
        Task<TokenData> GetUserToken();
    }

    public class TokenData
    {
        public string status { get; set; }
        public string token { get; set; }
    }
}
