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
using System.Windows.Data;

namespace DailyPoetryWPF.ViewModels
{
    class MyFavoritePageViewModel : BindableBase
    {
        private readonly IPoetryService poetryService;
        private readonly IRegionManager regionManager;
        private Favorite favorite;

        private ObservableCollection<Work> favoritePoetryList;
        public ObservableCollection<Work> FavoritePoetryList
        {
            get { return favoritePoetryList; }
            set { SetProperty(ref favoritePoetryList, value); }
        }

        public DelegateCommand<Work> FavoriteCommand { get; private set; }
        private async void favoriteCommand(Work work)
        {
            favorite = new Favorite();
            favorite.PoetryId = work.Id;
            await poetryService.DeleteFavorite(favorite);
            FavoritePoetryList.Remove(work);
        }

        public DelegateCommand LoadCommand { get; private set; }
        async void Load()
        {
            var list = await poetryService.GetFavoritePoetry();
            FavoritePoetryList.Clear();
            foreach (var item in list)
            {
                FavoritePoetryList.Add(item);
            }

        }

        public DelegateCommand<Work?> NavigateToDetailCommand;
        public void navigateToDetailCommand(Work? work)
        {
            var parameters = new NavigationParameters();
            parameters.Add("poetry", work);
            regionManager.RequestNavigate("ContentRegion", "DetailPage", parameters);
        }


        public MyFavoritePageViewModel(IPoetryService _poetryService, IRegionManager _regionManager)
        {
            LoadCommand = new DelegateCommand(Load);
            FavoriteCommand = new DelegateCommand<Work>(favoriteCommand);
            NavigateToDetailCommand = new DelegateCommand<Work?>(navigateToDetailCommand);
            poetryService = _poetryService;
            FavoritePoetryList = new ObservableCollection<Work>();
            regionManager = _regionManager;
        }

    }
}
