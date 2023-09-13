using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DailyPoetryWPF.Services;
using DailyPoetryWPF.Views;
using Prism.Ioc;
using Prism.Unity;

namespace DailyPoetryWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
            containerRegistry.RegisterForNavigation<RecommendPage>();

            containerRegistry.RegisterInstance<IRecommendItemService>(new RecommendItemService());
            //containerRegistry.RegisterInstance<IBingImageService>(new BingImageService());
            containerRegistry.RegisterInstance<ILocalInfoService>(new LocalInfoService());
            containerRegistry.RegisterInstance<IPoetryService>(new PoetryService());
        }
    }
}
