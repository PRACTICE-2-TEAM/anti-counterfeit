using Anticontrafact2.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anticontrafact2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public FirstPage()
        {
            InitializeComponent();
            
        }

        private void ToReporOnShop(object sender, EventArgs e)
        {
           RootPage.Menu.ToPage(MenuItemType.ReportOnShop);
        }

        private  void ToReportOnGood(object sender, EventArgs e)
        {
            RootPage.Menu.ToPage(MenuItemType.ReportOnGood);
        }

        private  void ToCheckShop(object sender, EventArgs e)
        {
            RootPage.Menu.ToPage(MenuItemType.CheckShop);
        }

        private  void ToCheckGood(object sender, EventArgs e)
        {
            RootPage.Menu.ToPage(MenuItemType.CheckGood);
        }
    }
}