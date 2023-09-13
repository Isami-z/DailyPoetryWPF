using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DailyPoetryWPF.Services
{
    public interface IBingImageService
    {
        Task<BitmapImage> GetBingImageDataAsync();
    }

}
