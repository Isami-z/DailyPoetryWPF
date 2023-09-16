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
    /// Interaction logic for SearchResultPage.xaml
    /// </summary>
    public partial class SearchResultPage : Page
    {
        public SearchResultPage()
        {
            InitializeComponent();
        }
        

        // 展开收起按钮的图形改变
        private void ChevronButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChevronIcon.Text == "\uE70E") // xaml 和 c# 表示十六进制不一样
            {
                ChevronIcon.Text = "\uE70D";
                ChevronText.Text = "展开";
            }
            else
            {
                ChevronIcon.Text = "\uE70E";
                ChevronText.Text = "收起";
            }
            AddButton.IsEnabled = !AddButton.IsEnabled;
        }
    }
}
