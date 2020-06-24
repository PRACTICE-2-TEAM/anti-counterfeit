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
            // Валидация введенных значений
            if (string.IsNullOrEmpty(INNNumber))
            {
                await page.DisplayAlert(null, "Введите ИНН в текстовое поле", "Принять");
                return;
            }

            // Проверка
            var api = AntiCounterfeitApiService.getInstance().Api;
            var outletInfo = await api.GetOutletInformation(INNNumber);
            await page.DisplayAlert(null, outletInfo.Result, "Принять");
        }
    }
}
