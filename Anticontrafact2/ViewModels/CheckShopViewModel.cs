using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class CheckShopViewMode:BaseViewModel
    {
        private CheckShopPage page;
        public CheckShopViewMode(CheckShopPage page)
        {
            this.page = page;
            CheckShopCommand = new Command(CheckShop);
        }

        public ICommand CheckShopCommand { get; }
        //TODO Добвавить команду открыть сканер сканера
        public string INNNumber { get; set; }


        private async void CheckShop()
        {
            await page.DisplayAlert("", "Button work\n code number - " + INNNumber, "OK");
        }

    }




}
