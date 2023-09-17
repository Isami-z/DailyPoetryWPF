using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace DailyPoetryWPF.ViewModels
{
    class SearchResultPageViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private IPoetryService poetryService;

        private List<Work> originalPoetryList;
        private int pageSize = 15;

        public string SearchUrl
        {
            get
            {
                string query = "";
                foreach (var item in FilterList)
                {
                    query += item.Content;
                }

                var searchUri = "https://cn.bing.com/search?q=" + query;
                return searchUri;
            }
        }

        private int pageCnt;
        public int PageCnt
        {
            get { return pageCnt; }
            set { SetProperty(ref pageCnt, value); }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { SetProperty(ref currentPage, value); }
        }

        //private List<int> pageIndex;
        //public List<int> PageIndex
        //{
        //    get { return pageIndex; }
        //    set { SetProperty(ref pageIndex, value); }
        //}

        private bool filterListVisibility;
        public bool FilterListVisibility
        {
            get { return filterListVisibility; }
            set { SetProperty(ref filterListVisibility, value); }
        }

        private bool simplifiedFilterListVisibility;
        public bool SimplifiedFilterListVisibility
        {
            get { return simplifiedFilterListVisibility; }
            set { SetProperty(ref simplifiedFilterListVisibility, value); }
        }

        private bool poetryResultVisibility;
        public bool PoetryResultVisibility
        {
            get { return poetryResultVisibility; }
            set { SetProperty(ref poetryResultVisibility, value); }
        }

        private bool resultScrollBarVisibility;
        public bool ResultScrollBarVisibility
        {
            get { return resultScrollBarVisibility; }
            set { SetProperty(ref resultScrollBarVisibility, value); }
        }

        private bool noResultTipVisibility;
        public bool NoResultTipVisibility
        {
            get { return noResultTipVisibility; }
            set { SetProperty(ref noResultTipVisibility, value); }
        }

        private bool prevButtonEnabled;
        public bool PrevButtonEnabled
        {
            get { return prevButtonEnabled; }
            set { SetProperty(ref prevButtonEnabled, value); }
        }

        private bool nextButtonEnabled;
        public bool NextButtonEnabled
        {
            get { return nextButtonEnabled; }
            set { SetProperty(ref nextButtonEnabled, value); }
        }

        private ObservableCollection<FilterItem> filterList;
        public ObservableCollection<FilterItem> FilterList
        {
            get { return filterList; }
            set { SetProperty(ref filterList, value); }
        }

        //private ObservableCollection<FilterItem> simplifiedFilterList;
        //public ObservableCollection<FilterItem> SimplifiedFilterList
        //{
        //    get { return simplifiedFilterList; }
        //    set { SetProperty(ref simplifiedFilterList, value); }
        //}

        private ObservableCollection<Work> poetryList;
        public ObservableCollection<Work> PoetryList
        {
            get { return poetryList; }
            set { SetProperty(ref poetryList, value); }
        }

        private void updateFilterListIndex()
        {
            var i = 0;
            foreach (var item in FilterList) { item.Index = i++; }
        }


        public DelegateCommand AddFilterItemCommand { get; private set; }
        private void addFilterItemCommand()
        {
            FilterList.Add(new FilterItem(FilterCategory.Content, ""));
            updateFilterListIndex();
        }

        public DelegateCommand<int?> DeleteFilterItemCommand { get; private set; }
        private void deleteFilterItemCommand(int? index)
        {
            FilterList.RemoveAt(index.Value);
            updateFilterListIndex();
        }

        public DelegateCommand ChevronCommand { get; private set; }
        private void chevronCommand()
        {
            FilterListVisibility = !FilterListVisibility;
            SimplifiedFilterListVisibility = !FilterListVisibility;
            foreach (var item in FilterList)
            {
                if (item.Content != "")
                {
                    return;
                }
            }
            SimplifiedFilterListVisibility = false;
        }

        public DelegateCommand SearchCommand { get; private set; }
        private void searchCommand()
        {
            NoResultTipVisibility = false;
            PoetryResultVisibility = false;
            ResultScrollBarVisibility = false;

            DoSearch();


        }

        //public DelegateCommand SearchFromInternetCommand { get; private set; }
        //private void searchFromInternetCommand()
        //{
        //    string query = "";
        //    foreach (var item in FilterList)
        //    {
        //        query += item.Content;
        //    }

        //    var searchUri = "https://cn.bing.com/search?q=" + query;
        //    Process.Start("https://www.bing.com");
        //}


        public DelegateCommand PrevPageCommand { get; private set; }
        public DelegateCommand NextPageCommand { get; private set; }
        public DelegateCommand RefreshPageCommand { get; private set; }

        private void prevPageCommand()
        {
            if (originalPoetryList != null)
            {
                PoetryList.Clear();
                CurrentPage -= 1;
                if (CurrentPage == 1)
                {
                    PrevButtonEnabled = false;
                }
                NextButtonEnabled = true;
                foreach (var item in originalPoetryList.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList())
                {
                    PoetryList.Add(item);
                }
            }
        }
        private void nextPageCommand()
        {
            if (originalPoetryList != null)
            {
                PoetryList.Clear();
                CurrentPage += 1;
                if (CurrentPage == PageCnt)
                {
                    NextButtonEnabled = false;
                }
                PrevButtonEnabled = true;
                foreach (var item in originalPoetryList.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList())
                {
                    PoetryList.Add(item);
                }
            }
        }

        private void refreshPageCommand()
        {

            if (CurrentPage == 1)
            {
                PrevButtonEnabled = false;
            }
            if (CurrentPage == PageCnt)
            {
                NextButtonEnabled = false;
            }
            if (originalPoetryList != null)
            {
                PoetryList.Clear();

                foreach (var item in originalPoetryList.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList())
                {
                    PoetryList.Add(item);
                }
                if (PoetryList.Count == 0)
                {
                    NoResultTipVisibility = true;
                }
                else
                {
                    NoResultTipVisibility = false;
                    PoetryResultVisibility = true;
                    if (PageCnt > 1)
                    {
                        ResultScrollBarVisibility = true;
                        PrevButtonEnabled = false;
                        NextButtonEnabled = true;
                    }
                }
            }
        }

        public DelegateCommand<Work?> NavigateToDetailCommand;
        public void navigateToDetailCommand(Work? work)
        {
            var parameters = new NavigationParameters();
            parameters.Add("poetry", work);
            regionManager.RequestNavigate("ContentRegion", "DetailPage", parameters);
        }

        private async void DoSearch()
        {
            originalPoetryList = await poetryService.GetWorkList();
            foreach (var item in FilterList)
            {
                switch (item.Category)
                {
                    case FilterCategory.Content:
                        poetryService.GetPoetryListByContent(ref originalPoetryList, item.Content);
                        break;
                    case FilterCategory.Title:
                        poetryService.GetPoetryListByName(ref originalPoetryList, item.Content);
                        break;
                    case FilterCategory.Author:
                        poetryService.GetPoetryListByAuthor(ref originalPoetryList, item.Content);
                        break;
                    default:
                        break;
                }
            }
            PageCnt = (originalPoetryList.Count + pageSize - 1) / pageSize;
            PageCnt = PageCnt > 1 ? PageCnt : 1;
            CurrentPage = 1;
            refreshPageCommand();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["content"] != null)
            {
                var content = ((string)navigationContext.Parameters["content"]).Trim();
                var work = originalPoetryList.Where(item => { return item.Content.Contains(content); }).FirstOrDefault();
                navigateToDetailCommand(work);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public SearchResultPageViewModel(IPoetryService _poetryService,
            IRegionManager _regionManager)
        {
            poetryService = _poetryService;
            regionManager = _regionManager;
            AddFilterItemCommand = new DelegateCommand(addFilterItemCommand);
            DeleteFilterItemCommand = new DelegateCommand<int?>(deleteFilterItemCommand);
            ChevronCommand = new DelegateCommand(chevronCommand);
            SearchCommand = new DelegateCommand(searchCommand);
            PrevPageCommand = new DelegateCommand(prevPageCommand);
            NextPageCommand = new DelegateCommand(nextPageCommand);
            //SearchFromInternetCommand = new DelegateCommand(searchFromInternetCommand);
            RefreshPageCommand = new DelegateCommand(refreshPageCommand);
            NavigateToDetailCommand = new DelegateCommand<Work?>(navigateToDetailCommand);

            FilterListVisibility = true;
            SimplifiedFilterListVisibility = false;

            FilterList = new ObservableCollection<FilterItem>();
            //SimplifiedFilterList = new ObservableCollection<FilterItem>();
            FilterList.Add(new FilterItem(FilterCategory.Content, ""));

            PoetryList = new ObservableCollection<Work>();
        }
    }



    public enum FilterCategory
    {
        Title, Content, Author
    }

    public class FilterItem : BindableBase
    {
        private int index;
        private string content;
        private FilterCategory category;

        public int Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public FilterCategory Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }

        public FilterItem(FilterCategory filterCategory, string value)
        {
            Index = 0;
            Category = filterCategory;
            Content = value;
        }

        public FilterItem()
        {
            Category = FilterCategory.Title;
            Content = "月光";
            index = 0;
        }
    }

    public class FilterCategoryStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case FilterCategory.Title: return "标题";
                case FilterCategory.Author: return "作者";
                case FilterCategory.Content: return "内容";
                default: throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "标题": return FilterCategory.Title;
                case "内容": return FilterCategory.Content;
                case "作者": return FilterCategory.Author;
                default: throw new NotImplementedException();
            }
        }
    }

}
