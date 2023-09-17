using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DailyPoetryWPF.ViewModels
{
    public class DetailPageViewModel : BindableBase, INavigationAware
    {
        private IPoetryService poetryService;

        private Favorite favorite = new Favorite();
        private RecentView recentView = new RecentView();

        private Work poetryItem;
        public Work PoetryItem
        {
            get
            {
                return poetryItem;
            }
            set { SetProperty(ref poetryItem, value); }
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set { SetProperty(ref isFavorite, value); }
        }

        public DelegateCommand FavoriteCommand { get; private set; }
        private async void favoriteCommand()
        {
            if (IsFavorite == true)
            {
                favorite.PoetryId = PoetryItem.Id;
                await poetryService.InsertFavorite(favorite);
            }
            else
            {
                favorite.PoetryId = PoetryItem.Id;
                favorite = await poetryService.IsFavorite((int)favorite.PoetryId);
                await poetryService.InsertFavorite(favorite);
            }
        }

        public DetailPageViewModel(IPoetryService _poetryService)
        {
            poetryService = _poetryService;

            FavoriteCommand = new DelegateCommand(favoriteCommand);
            //poetryService.GetWorkById(10001).ContinueWith(t =>
            //{
            //    PoetryItem = t.Result; poetryItem.Annotation = OptimizeChineseText(poetryItem.Annotation);
            //    poetryItem.Translation = OptimizeChineseText(poetryItem.Translation);
            //    poetryItem.Appreciation = OptimizeChineseText(poetryItem.Appreciation);
            //});
        }

        private string OptimizeChineseText(string rawString)
        {
            if (rawString != null)
            {
                var splitted = rawString.Split('\n');
                string result = "    " + splitted[0];
                foreach (var line in splitted)
                {
                    result += "\n    " + line;
                }
                return result;
            }
            else
            {
                return rawString;
            }

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            //在这里处理导航到该页面时的逻辑
            if (navigationContext.Parameters["poetry"] != null)
            {

                PoetryItem = (Work)navigationContext.Parameters["poetry"];
                poetryItem.Annotation = OptimizeChineseText(poetryItem.Annotation);
                poetryItem.Translation = OptimizeChineseText(poetryItem.Translation);
                poetryItem.Appreciation = OptimizeChineseText(poetryItem.Appreciation);

                favorite = await poetryService.IsFavorite((int)poetryItem.Id);
                if (favorite != null)
                {
                    IsFavorite = true;
                }
                else
                {
                    IsFavorite = false;
                    favorite = new Favorite();
                }

                recentView = await poetryService.IsRecent((int)PoetryItem.Id);
                if (recentView != null)
                {
                    await poetryService.DeleteRecent(recentView);
                    recentView = new RecentView();
                    recentView.PoetryItemId = PoetryItem.Id;
                    await poetryService.InsertRecent(recentView);
                }
                else
                {
                    recentView = new RecentView();
                    recentView.PoetryItemId = PoetryItem.Id;
                    await poetryService.InsertRecent(recentView);
                }
            }
        }
    }
}
