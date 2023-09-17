using DailyPoetryWPF.Helpers;
using DailyPoetryWPF.Models;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace DailyPoetryWPF.Services
{
    public class BingImageService : IBingImageService
    {
        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private string imagePath = "bingimg";
        private SQLiteAsyncConnection connection;
        public async Task<bool> IsUpdateNeeded()
        {
            var startdate = await connection.FindAsync<PreferenceStorage>(t => t.Key == "fullstartdate");
            if (startdate != null)
            {
                string fullstartdate = startdate.Value;
                if (DateTime.ParseExact(fullstartdate, "yyyyMMddHHmm", CultureInfo.CurrentCulture).AddDays(1) < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            PreferenceStorage storage = new PreferenceStorage();
            storage.Key = "fullstartdate";
            storage.Value = DateTime.MinValue.ToString("yyyyMMddHHmm");
            await connection.InsertAsync(storage);
            return true;
        }

        public async Task<BitmapImage> GetBingImageDataAsync()
        {
            connection = DatabaseHelper.ConnectDatabase();
            // 判断是否需要从网络更新

            bool shouldUpdate = await IsUpdateNeeded();
            if (shouldUpdate)
            {
                string strRegion = "zh-CN";
                string strBingImageURL = $"https://www.cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n={1}&mkt={strRegion}";
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(strBingImageURL);
                string json = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(json);
                PreferenceStorage storage = new PreferenceStorage();
                storage.Key = "fullstartdate";
                storage.Value = jObject["images"][0]["fullstartdate"].ToString();
                await connection.UpdateAsync(storage);

                using var iresponse = await HttpHelper.GetResponseAsync($"http://www.bing.com{jObject["images"][0]["url"]}");
                var data = await iresponse.Content.ReadAsByteArrayAsync();
                using (FileStream fs = new FileStream(Path.Combine(folderPath, imagePath), FileMode.Create))
                {
                    fs.Write(data, 0, data.Length);
                }
                using var stream = new MemoryStream(data);
                var image = new BitmapImage();
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();

                return image;
            }
            else
            {
                using (FileStream fs = new FileStream(Path.Combine(folderPath, imagePath), FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);

                    using var stream = new MemoryStream(bytes);
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = stream;
                    image.EndInit();
                    image.Freeze();

                    return image;

                }
            }
        }
    }
}
