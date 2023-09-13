using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.Services
{
    public class TokenService : ITokenService
    {
        public async Task<TokenData> GetUserToken()
        {
            var httpClient = new HttpClient();
            string json = "";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            const string dataName = "\\JinrishiciToken.dat";

            try
            {
                var filePath = folderPath + dataName;
                if (!File.Exists(filePath)) { File.Create(filePath); }
                using (var fs = File.Open(filePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string text = sr.ReadToEnd();
                        if (text == string.Empty)
                        {
                            var response = await httpClient.GetAsync("https://v2.jinrishici.com/token");
                            json = await response.Content.ReadAsStringAsync();
                            JObject jObject = JObject.Parse(json);
                            TokenData result = new TokenData();
                            result.status = "success";
                            result.token = jObject["data"].ToString();
                            await fs.WriteAsync(Encoding.UTF8.GetBytes(result.token));
                            return result;
                        }
                        else
                        {
                            //byte[] bytes = new byte[1024];
                            //int bytesRead = await fs.ReadAsync(bytes, 0, 1024);
                            //Encoding.UTF8.GetString(bytes, 0, bytesRead);
                           
                            TokenData result = new TokenData();
                            result.status = "success";
                            result.token = text;
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TokenData tokenData = new TokenData();
                tokenData.status = "error";
                return tokenData;
            }
        }
    }
}
