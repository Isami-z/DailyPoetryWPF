using DailyPoetryWPF.Helpers;
using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DailyPoetryWPF.ViewModels
{
    class RecommendPageViewModel : BindableBase
    {
        public DelegateCommand LoadCommand { get; private set; }

        private IRecommendItemService recommendItemService;
        // private IBingImageService bingImageService;
        private ILocalInfoService localInfoService;
        private IPoetryService poetryService;

        private RecommendItem recommendItem;
        private BitmapImage bingImg;
        private Origin origin;
        private LocalInfoData localInfoData;
        private Author author;

        public Author Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        public LocalInfoData LocalInfoData
        {
            get { return localInfoData; }
            set { SetProperty(ref localInfoData, value); }
        }

        public Origin Origin
        {
            get { return origin; }
            set { SetProperty(ref origin, value); }
        }

        public BitmapImage BingImg
        {
            get { return bingImg; }
            set
            {
                SetProperty(ref bingImg, value);
            }
        }

        public RecommendItem RecommendItem
        {
            get { return recommendItem; }
            set { SetProperty(ref recommendItem, value); }
        }

        public RecommendPageViewModel(IRecommendItemService _recommendItemService, 
            ILocalInfoService _localInfoService,
            IPoetryService _poetryService)
        {
            poetryService = _poetryService;
            localInfoService = _localInfoService;
            recommendItemService = _recommendItemService;
            LoadCommand = new DelegateCommand(Load);
        }



        async void Load()
        {
           
            //BingImg = await bingImageService.GetBingImageDataAsync();
            LocalInfoData = await localInfoService.GetLocalInfoAsync();
            RecommendItem = await recommendItemService.GetRecommendContentAsync();
            Origin = RecommendItem.origin;
            Author = await poetryService.GetAuthorById(10001);
        }
    }
}
