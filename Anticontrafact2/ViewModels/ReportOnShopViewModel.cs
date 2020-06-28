using Anticontrafact2.Api;
using Anticontrafact2.Models;
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
            // Валидация введенных значений
            if (string.IsNullOrEmpty(ShopName) || string.IsNullOrEmpty(AdressText) || string.IsNullOrEmpty(INNNumber) || string.IsNullOrEmpty(CauseDiscriptionText))
            {
                await page.DisplayAlert(null, "Заполните текстовые все поля", "Принять");
                return;
            }

            // Формируем заявку
            var data = new ComplaintOutputData
            {
                Token = User.GetUser().Token,
                Description = "ИНН: " + INNNumber + "\n" + CauseDiscriptionText,
                Address = AdressText,
                Unit = "",
                Type = "sale-point",
                Status = "В обработке"
            };

            // Отправка жалобы
            var api = AntiCounterfeitApiService.getInstance().Api;
            var complaintResult = await api.Complain(data);
            if (!complaintResult.Success)
            {
                await page.DisplayAlert(null, complaintResult.Reason, "Принять");
                return;
            }
            await page.DisplayAlert(null, "Ваша заявка принята для обработки", "Принять");

            ShopName = AdressText = INNNumber = CauseDiscriptionText = "";
        }
    }
}
