using Anticontrafact2.Api;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            // Если поле с ИНН не пустое, ...
            if (!string.IsNullOrWhiteSpace(INNNumber))
            {
                // то запрашиваем данные у сервера ...
                OutletInfo outletInfo = await AntiCounterfeitApiService.getInstance().Api.CheckOutlet(INNNumber);
                // и выводим результат на экран.
                await page.DisplayAlert("", outletInfo.Result, "OK");
            }
        }
    }
}
