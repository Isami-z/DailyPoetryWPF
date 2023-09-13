using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public class LocalInfoService : ILocalInfoService
    {
        public async Task<LocalInfoData> GetLocalInfoAsync()
        {
            ITokenService tokenService = new TokenService();
            var tokenRes = await tokenService.GetUserToken();
            var token = tokenRes.token;

            var httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            headers.Add("X-User-Token", token);

            string jString = "";
            try
            {
                //Send the GET request
                var response = await httpClient.GetAsync("https://v2.jinrishici.com/info");
                response.EnsureSuccessStatusCode();
                jString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                jString = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return JsonConvert.DeserializeObject<LocalInfoObject>(jString).data;
        }
    }
}
