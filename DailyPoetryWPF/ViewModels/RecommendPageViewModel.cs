using DailyPoetryWPF.Helpers;
using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using DailyPoetryWPF.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DailyPoetryWPF.ViewModels
{
    class RecommendPageViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand LoadCommand { get; private set; }

        private readonly IRegionManager regionManager;

        private IRecommendItemService recommendItemService;
        private IBingImageService bingImageService;
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

        public DelegateCommand NavigateToDetailCommand { get; private set; }

        private void NavigateToDetail()
        {
            regionManager.RequestNavigate("ContentRegion", "DetailPage");
        }

        public RecommendPageViewModel(IRecommendItemService _recommendItemService, 
            ILocalInfoService _localInfoService,
            IPoetryService _poetryService,
            IBingImageService _bingImageService,
            IRegionManager _regionManager)
        {
            poetryService = _poetryService;
            localInfoService = _localInfoService;
            recommendItemService = _recommendItemService;
            LoadCommand = new DelegateCommand(Load);
            bingImageService = _bingImageService;
            regionManager = _regionManager;

            NavigateToDetailCommand = new DelegateCommand(NavigateToDetail, CanNavigate);
        }

        private bool CanNavigate()
        {
            //在这里判断是否可以执行导航，返回true或false
            return true;
        }

        async void Load()
        {
           
            BingImg = await bingImageService.GetBingImageDataAsync();
            LocalInfoData = await localInfoService.GetLocalInfoAsync();
            RecommendItem = await recommendItemService.GetRecommendContentAsync();
            Origin = RecommendItem.origin;
            Author = await poetryService.GetAuthorById(10001);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
