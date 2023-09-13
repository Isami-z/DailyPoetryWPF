//using DailyPoetryWPF.Helpers;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Media.Imaging;
//using static System.Net.WebRequestMethods;

//namespace DailyPoetryWPF.Services
//{
//    public class BingImageService : IBingImageService
//    {
//        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
//        public Task<bool> CheckUpdateAsync()
//        {
//            BingImage bingImage = ReadBingImageAsync();
//            if (bingImage == null)
//            {
//                return Task.FromResult(true);
//            }
//            else
//            {
//                DateTime dateTime = DateTime.ParseExact(bingImage.FullStartDate, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
//                if (DateTime.Now > dateTime.AddDays(1))
//                {
//                    return Task.FromResult(true);
//                }
//                return Task.FromResult(true);
//            }

//        }

//        public BingImage ReadBingImageAsync()
//        {
//            string fileName = Path.Combine(folderPath, "bingimg");

//            try
//            {
//                if (System.IO.File.Exists(fileName))
//                {
//                    BinaryFormatter formatter = new BinaryFormatter();

//                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
//                    {
//                        BingImage data = (BingImage)formatter.Deserialize(fileStream);
//                        return data;
//                    }
//                }
//                else
//                {
//                    return null; // 文件不存在
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}");
//                return null;
//            }
//        }

//        public void SaveBingImage(BingImage bingImage, BitmapImage image)
//        {

//            string fileName = Path.Combine(folderPath, "bingimg");
//            string imageName = Path.Combine(folderPath, "bingimg.jpg");
//            try
//            {
//                if (!System.IO.File.Exists(fileName))
//                {
//                    System.IO.File.Create(fileName);
//                    System.IO.File.Create(imageName);
//                }
//                BinaryFormatter formatter = new BinaryFormatter();
//                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
//                {
//                    formatter.Serialize(fileStream, bingImage);
//                }
//                using (FileStream fileStream = new FileStream(imageName, FileMode.Open)
//                {
                    
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<BitmapImage> GetBingImageDataAsync()
//        {
//            bool shouldUpate = await CheckUpdateAsync();
//            if (shouldUpate)
//            {
//                string strRegion = "zh-CN";
//                string strBingImageURL = $"http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n={1}&mkt={strRegion}";
//                var httpClient = new HttpClient();
//                var response = await httpClient.GetAsync(strBingImageURL);
//                string json = await response.Content.ReadAsStringAsync();
//                JObject jObject = JObject.Parse(json);
//                BingImage image = new BingImage();
//                image.FullStartDate = jObject["images"][0]["fullstartdate"].ToString();
//                image.Url = jObject["images"][0]["url"].ToString();
//                BitmapImage bi = await HttpHelper.GetImageAsync("http://www.bing.com" + image.Url);
//                return bi;
//            }
//            else
//            {
//                BingImage bingImage = ReadBingImageAsync();
//            }
//        }
//    }
//}
