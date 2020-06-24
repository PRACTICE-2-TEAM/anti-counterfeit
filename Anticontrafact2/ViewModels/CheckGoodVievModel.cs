using Anticontrafact2.Api;
using Anticontrafact2.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class CheckGoodVievModel:BaseViewModel
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
            // Если поле с номером штрих-кода не пустое, ...
            if (!string.IsNullOrWhiteSpace(CodeNumber))
            {
                // то запрашиваем данные у сервера ...
                BarcodeInfo barcodeInfo = await AntiCounterfeitApiService.getInstance().Api.CheckBarcode(CodeNumber);
                // и выводим результат на экран.
                await page.DisplayAlert("", barcodeInfo.Result, "OK");
            }
        }
        
        private void ScanCode()
        {
        }
    }
}
