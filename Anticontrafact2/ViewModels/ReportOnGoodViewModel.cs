using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Anticontrafact2.ViewModels
{
    class ReportOnGoodViewModel : BaseViewModel
    {
        private ReportOnGoodPage page;
        public ReportOnGoodViewModel(ReportOnGoodPage page)
        {
            this.page = page;

            ScanCodeCommand = new Command(ScanCode);
            SendReportCommand = new Command(SendReport);
        }

        public ICommand ScanCodeCommand { get; }
        public ICommand SendReportCommand { get; }

        private string _productName;
        private string _codeNumber;
        private string _causeDiscriptionText;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string CodeNumber
        {
            get => _codeNumber;
            set
            {
                _codeNumber = value;
                OnPropertyChanged(nameof(CodeNumber));
            }
        }

        public string CauseDiscriptionText
        {
            get => _causeDiscriptionText;
            set
            {
                _causeDiscriptionText = value;
                OnPropertyChanged(nameof(CauseDiscriptionText));
            }
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

        private async void SendReport()
        {
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            // Валидация введенных значений
            if (string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(CodeNumber) || string.IsNullOrEmpty(CauseDiscriptionText))
            {
                await page.DisplayAlert(null, "Заполните текстовые все поля", "Принять");
                return;
            }

            // Формируем заявку
            var data = new ComplaintOutputData
            {
                Token = User.GetUser().Token,
                Description = "Название: " + ProductName + "\nНомер штрих-кода: " + CodeNumber + "\nОписание:\n" + CauseDiscriptionText,
                Address = "", // <-- TODO
                Unit = "", // <-- TODO
                Type = "product",
                Status = "На рассмотрении"
            };

            // Отправка жалобы
            var complaintResult = await api.Complain(data);
            if (!complaintResult.Success)
            {
                await page.DisplayAlert(null, complaintResult.Reason, "Принять");
                return;
            }
            await page.DisplayAlert(null, "Ваша заявка отправлена на рассмотрение", "Принять");

            // Очищаем поля
            ProductName = CodeNumber = CauseDiscriptionText = "";
        }
    }
}
