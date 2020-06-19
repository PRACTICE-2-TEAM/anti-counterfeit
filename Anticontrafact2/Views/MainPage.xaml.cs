using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Anticontrafact2.Models;

namespace Anticontrafact2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
       
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.CheckGood, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.CheckGood:
                        MenuPages.Add(id, new NavigationPage(new CheckGoodPage()));
                        break;
                    case (int)MenuItemType.CheckShop:
                        MenuPages.Add(id, new NavigationPage(new CheckShopPage()));
                        break;
                    case (int)MenuItemType.ReportOnGood:
                        MenuPages.Add(id, new NavigationPage(new ReportOnGoodPage()));
                        break;
                    case (int)MenuItemType.ReportOnShop:
                        MenuPages.Add(id, new NavigationPage(new ReportOnShopPage()));
                        break;
                    case (int)MenuItemType.ReportsStatus:
                        MenuPages.Add(id, new NavigationPage(new ReportsStatusPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}