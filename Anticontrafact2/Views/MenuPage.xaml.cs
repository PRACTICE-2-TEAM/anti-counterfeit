using Anticontrafact2.Models;
using Anticontrafact2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anticontrafact2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        List<HomeMenuItem> MenuAccActs;

        
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(this);

           

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.CheckGood, Title="Проверить товар" },
                new HomeMenuItem {Id = MenuItemType.CheckShop, Title="Проверить точку" },
                new HomeMenuItem {Id = MenuItemType.ReportOnGood, Title="Пожаловаться на товар" },
                new HomeMenuItem {Id = MenuItemType.ReportOnShop, Title="Пожаловаться на точку" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            MenuAccActs = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.ReportsStatus, Title="Статус текущих жалоб" },
            };

            ListViewMenuAccActs.ItemsSource = MenuAccActs;


            ListViewMenuAccActs.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }

    }
}