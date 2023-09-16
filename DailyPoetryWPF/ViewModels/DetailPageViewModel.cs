using DailyPoetryWPF.Models;
using DailyPoetryWPF.Services;
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

        private Work poetryItem;
        public Work PoetryItem
        {
            get
            {
                return poetryItem;
            }
            set { SetProperty(ref poetryItem, value); }
        }

        public DetailPageViewModel(IPoetryService _poetryService)
        {
            poetryService = _poetryService;
            poetryService.GetWorkById(10001).ContinueWith(t => { PoetryItem = t.Result; poetryItem.Annotation = OptimizeChineseText(poetryItem.Annotation);
                poetryItem.Translation = OptimizeChineseText(poetryItem.Translation);
                poetryItem.Appreciation = OptimizeChineseText(poetryItem.Appreciation);
            });
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //在这里处理导航到该页面时的逻辑
            if (navigationContext.Parameters["poetry"] != null)
            {
                PoetryItem = (Work)navigationContext.Parameters["poetry"];
                poetryItem.Annotation = OptimizeChineseText(poetryItem.Annotation);
                poetryItem.Translation = OptimizeChineseText(poetryItem.Translation);
                poetryItem.Appreciation = OptimizeChineseText(poetryItem.Appreciation);
            }
        }
    }
}
