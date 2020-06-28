using Anticontrafact2.Api;
using Anticontrafact2.Views;
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
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            // Валидация введенных значений
            if (string.IsNullOrEmpty(INNNumber))
            {
                await page.DisplayAlert(null, "Введите ИНН в текстовое поле", "Принять");
                return;
            }

            // Проверка
            var outletInfo = await api.GetOutletInformation(INNNumber);
            await page.DisplayAlert(null, outletInfo.Result, "Принять");
        }
    }
}
