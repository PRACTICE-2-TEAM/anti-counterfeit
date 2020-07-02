using Anticontrafact2.Api;
using Anticontrafact2.Views;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Anticontrafact2.ViewModels
{
    class CheckGoodVievModel : BaseViewModel
    {
        CheckGoodPage page;

        public CheckGoodVievModel(CheckGoodPage page)
        {
            this.page = page;

            CheckGoodCommand = new Command(CheckGood);
            ScanCodeCommand = new Command(ScanCode);
        }

        public ICommand CheckGoodCommand { get; }
        public ICommand ScanCodeCommand { get; }

        public string _codeNumber;

        public string CodeNumber
        {
            get => _codeNumber;
            set
            {
                _codeNumber = value;
                OnPropertyChanged(nameof(CodeNumber));
            }
        }

        private async void CheckGood()
        {
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            // Валидация введенных значений
            if (string.IsNullOrWhiteSpace(CodeNumber))
            {
                await page.DisplayAlert(null, "Введите номер штрих-кода в текстовое поле", "Принять");
                return;
            }

            // Проверка
            var barcodeInfo = await api.GetProductInformation(CodeNumber);
            var message = barcodeInfo.Result;
            if (barcodeInfo.Info != null)
            {
                message += "\n\n" +
                    "Страна: " + (barcodeInfo.Info.Country ?? "неизвестно") + "\n" +
                    "Имя: " + (barcodeInfo.Info.Name ?? "неизвестно") + "\n" +
                    "Бренд: " + (barcodeInfo.Info.Brand ?? "неизвестно") + "\n" +
                    "Стоимость единицы: " + (barcodeInfo.Info.UnitValue ?? "неизвестно") + "\n" +
                    "Изделие: " + (barcodeInfo.Info.Article ?? "неизвестно");
            }
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
                CodeNumber = result.Text;
            });
        }
        /**/
    }
}
