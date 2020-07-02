using Anticontrafact2.Api;
using Anticontrafact2.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Anticontrafact2.ViewModels
{
    class CheckShopViewMode : BaseViewModel
    {
        private CheckShopPage page;
        public CheckShopViewMode(CheckShopPage page)
        {
            this.page = page;
            CheckShopCommand = new Command(CheckShop);
            ScanCodeCommand = new Command(ScanCode);
        }

        public ICommand CheckShopCommand { get; }
        public ICommand ScanCodeCommand { get; }

        private string _INNNumber;

        public string INNNumber
        {
            get => _INNNumber;
            set
            {
                _INNNumber = value;
                OnPropertyChanged(nameof(INNNumber));
            }
        }

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
            var message = outletInfo.Result + "\n\n" +
                "Название: " + (outletInfo.Name ?? "неизвестно") + "\n" +
                "Адрес: " + (outletInfo.Address ?? "неизвестно");
            await page.DisplayAlert(null, message, "Принять");
        }

        /* Сканирование штрих-кода */
        ZXingScannerPage scannerPage;

        private async void ScanCode()
        {
            if (scannerPage == null)
            {
                scannerPage = new ZXingScannerPage();
                scannerPage.OnScanResult += ScanPage_OnScanResult;
            }
            await page.Navigation.PushAsync(scannerPage);
        }

        private void ScanPage_OnScanResult(Result result)
        {
            scannerPage.IsScanning = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await page.Navigation.PopAsync();
                try
                {
                    var uri = new Uri(result.Text);
                    await Browser.OpenAsync(uri);
                }
                catch
                {
                    await page.DisplayAlert(null, "Не удалось получить ИНН\nПожалуйста, введите ИНН в указанное поле вручную", "Принять");
                }
            });
        }
        /**/
    }
}
