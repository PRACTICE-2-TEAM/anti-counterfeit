using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class ReportOnGoodViewModel
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

        public string ProductName { get; set; }
        public string CodeNumber { get; set; }
        public string CauseDiscriptionText { get; set; }

        private void ScanCode()
        {

        }
        private async void SendReport()
        {
            await page.DisplayAlert("", "data:\n" + ProductName + " " + CodeNumber + " " + CauseDiscriptionText, "OK");
        }


    }
}
