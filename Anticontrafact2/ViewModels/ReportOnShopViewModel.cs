using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class ReportOnShopViewModel :BaseViewModel
    {
        ReportOnShopPage page;
        public ReportOnShopViewModel(ReportOnShopPage page)
        {
            this.page = page;

            ScanCodeCommand = new Command(ScanCode);
            SendReportCommand = new Command(SendReport);
        }

        public ICommand ScanCodeCommand { get; }
        public ICommand SendReportCommand { get; }

        public string ShopName { get; set; }
        public string AdressText { get; set; }
        public string INNNumber { get; set; }
        public string CauseDiscriptionText { get; set; }

        private void ScanCode()
        {

        }
        private async void SendReport()
        {
            await page.DisplayAlert("", "data:\n" + ShopName + AdressText + INNNumber + CauseDiscriptionText, "OK");
        }
    }
}
