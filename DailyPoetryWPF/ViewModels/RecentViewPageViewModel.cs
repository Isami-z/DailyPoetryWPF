using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryWPF.ViewModels
{
    class RecentViewPageViewModel : BindableBase
    {
        private readonly IPoetryService poetryService;
        private readonly IRegionManager regionManager;
        private RecentView recent;

        private ObservableCollection<Work> recentPoetryList;
        public ObservableCollection<Work> RecentPoetryList
        {
            get { return recentPoetryList; }
            set { SetProperty(ref recentPoetryList, value); }
        }

        public DelegateCommand<Work> FavoriteCommand { get; private set; }
        private async void favoriteCommand(Work work)
        {
            recent = new RecentView();
            recent.PoetryItemId = work.Id;
            await poetryService.DeleteRecent(recent);
            RecentPoetryList.Remove(work);
        }

        public DelegateCommand LoadCommand { get; private set; }
        async void Load()
        {
            var list = await poetryService.GetRecentPoetry();
            RecentPoetryList.Clear();
            foreach (var item in list)
            {
                RecentPoetryList.Add(item);
            }
        }

        public DelegateCommand<Work?> NavigateToDetailCommand;
        public void navigateToDetailCommand(Work? work)
        {
            var parameters = new NavigationParameters();
            parameters.Add("poetry", work);
            regionManager.RequestNavigate("ContentRegion", "DetailPage", parameters);
        }

        public DelegateCommand ClearCommand { get; private set; }
        private async Task clearCommandAsync()
        {
            foreach (var item in RecentPoetryList)
            {
                var recent = new RecentView();
                recent.PoetryItemId = item.Id;
                await poetryService.DeleteRecent(recent);
            }
            RecentPoetryList.Clear();
        }

        public RecentViewPageViewModel(IPoetryService _poetryService, IRegionManager _regionManager)
        {
            LoadCommand = new DelegateCommand(Load);
            FavoriteCommand = new DelegateCommand<Work>(favoriteCommand);
            NavigateToDetailCommand = new DelegateCommand<Work?>(navigateToDetailCommand);
            ClearCommand = new DelegateCommand(async () => await clearCommandAsync()) ;
            poetryService = _poetryService;
            regionManager = _regionManager;
            RecentPoetryList = new ObservableCollection<Work>();
        }
    }
}
