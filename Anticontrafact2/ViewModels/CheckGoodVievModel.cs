using Anticontrafact2.Api;
using Anticontrafact2.Views;
using System.Windows.Input;
using Xamarin.Forms;

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
        public string CodeNumber { get; set; }

        private async void CheckGood()
        {
            // Валидация введенных значений
            if (string.IsNullOrWhiteSpace(CodeNumber))
            {
                await page.DisplayAlert(null, "Введите номер штрих-кода в текстовое поле", "Принять");
                return;
            }

            // Проверка
            var api = AntiCounterfeitApiService.getInstance().Api;
            var barcodeInfo = await api.GetProductInformation(CodeNumber);
            await page.DisplayAlert(null, barcodeInfo.Result, "Принять");
        }

        private void ScanCode()
        {
        }
    }
}
