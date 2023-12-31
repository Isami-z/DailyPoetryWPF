﻿using DailyPoetryWPF.Models;
using DailyPoetryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DailyPoetryWPF.Views
{
    /// <summary>
    /// Interaction logic for MyFavoritePage.xaml
    /// </summary>
    public partial class MyFavoritePage : Page
    {
        public MyFavoritePage()
        {
            InitializeComponent();
        }

        private void FavoritePoetryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MyFavoritePageViewModel)this.DataContext).navigateToDetailCommand((Work)FavoritePoetryList.SelectedItem);

        }
    }
}
