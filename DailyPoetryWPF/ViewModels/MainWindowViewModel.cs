using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DailyPoetryWPF.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private  IRegionNavigationJournal regionNavigationJournal;

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public MainWindowViewModel(IRegionManager _regionManager)
        {
            Title = "今日诗词";
            regionManager = _regionManager;
            LoadCommand = new DelegateCommand(Load);
            NavigateCommand = new DelegateCommand<string>(navigate); 
            BackCommand = new DelegateCommand(back);

            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(RecommendPage));
        }

        public DelegateCommand BackCommand { get; private set; }
        void back()
        { if (regionNavigationJournal != null && regionNavigationJournal.CanGoBack == true)
            {
                regionNavigationJournal.GoBack();

            }
        }
        bool canGoBack()
        {
            return regionNavigationJournal.CanGoBack;
        }


        public DelegateCommand<string> NavigateCommand { get; private set; }
        void navigate(string para)
        {
            regionNavigationJournal = regionManager.Regions["ContentRegion"].NavigationService.Journal;
            regionManager.RequestNavigate("ContentRegion", para);
        }


        public DelegateCommand LoadCommand { get; private set; }
        async void Load()
        {
            regionManager.AddToRegion("ContentRegion", "RecommendPage");
            regionManager.AddToRegion("ContentRegion", "DetailPage");
            regionManager.AddToRegion("ContentRegion", "MyCreation");
            regionManager.AddToRegion("ContentRegion", "MyFavoritePage");
            regionManager.AddToRegion("ContentRegion", "RecentViewPage");
            regionManager.AddToRegion("ContentRegion", "SearchResultPage");
            regionManager.AddToRegion("ContentRegion", "SettingsPage");
            regionManager.RequestNavigate("ContentRegion", "RecommendPage");

        }
    }
}
