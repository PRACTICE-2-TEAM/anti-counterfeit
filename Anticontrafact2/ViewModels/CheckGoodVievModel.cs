using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
            await page.DisplayAlert("", "Button work\n code number - "+CodeNumber, "OK");
        }
        private void ScanCode()
        {

        }

    }

   
}
