using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Anticontrafact2.ViewModels
{
    class ReportOnShopViewModel : BaseViewModel
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

        private string _shopName;
        private string _adressText;
        private string _INNNumber;
        private string _causeDiscriptionText;

        public string ShopName
        {
            get => _shopName;
            set
            {
                _shopName = value;
                OnPropertyChanged(nameof(ShopName));
            }
        }

        public string AdressText
        {
            get => _adressText;
            set
            {
                _adressText = value;
                OnPropertyChanged(nameof(AdressText));
            }
        }

        public string INNNumber
        {
            get => _INNNumber;
            set
            {
                _INNNumber = value;
                OnPropertyChanged(nameof(INNNumber));
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

        /* Сканирование QR-кода */
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
                await page.DisplayAlert(null, "Не удалось получить ИНН\nПожалуйста, введите ИНН в указанное поле вручную", "Принять"); // ахахах
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
            if (string.IsNullOrEmpty(ShopName) || string.IsNullOrEmpty(AdressText) || string.IsNullOrEmpty(INNNumber) || string.IsNullOrEmpty(CauseDiscriptionText))
            {
                await page.DisplayAlert(null, "Заполните текстовые все поля", "Принять");
                return;
            }

            // Формируем заявку
            var data = new ComplaintOutputData
            {
                Token = User.GetUser().Token,
                Description = "Название: " + ShopName + "\nИНН: " + INNNumber + "\nОписание:\n" + CauseDiscriptionText,
                Address = AdressText,
                Unit = "", // <-- TODO
                Type = "sale-point",
                Status = "На рассмотрении"
            };

            // Отправка жалобы
            var complaintResult = await api.Complain(data);
            if (!complaintResult.Success)
            {
                await page.DisplayAlert(null, complaintResult.Reason, "Принять");
                return;
            }
            await page.DisplayAlert(null, "Ваша заявка принята на рассмотрение", "Принять");

            ShopName = AdressText = INNNumber = CauseDiscriptionText = "";
        }
    }
}
