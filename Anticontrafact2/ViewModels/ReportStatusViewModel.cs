using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    public class ReportStatusViewModel : BaseViewModel
    {
        ReportsStatusPage page;
        public ObservableCollection<Report> Reports { get; set; }
        public Command LoadReportsCommand { get; set; }
        public ReportStatusViewModel()
        {
            Reports = new ObservableCollection<Report>();

            LoadReportsCommand = new Command(LoadReports);
            LoadReports();
        }

        private async void LoadReports()
        {
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            Reports.Clear();

            string token = User.GetUser().Token;
            var identifiers = await api.GetComplaintIdentifiers(token, 100, 1);
            foreach (var identifier in identifiers)
            {
                var data = await api.GetComplaintData(token, identifier.Id);
                if (data.Type == "product")
                {
                    Reports.Add(new Report {
                        TitleName = "Товар",
                        State = data.Status == "В обработке" ? ReportStatus.inProcessing : ReportStatus.ready,
                        Description = data.Description
                    });
                }
                else if (data.Type == "sale-point")
                {
                    Reports.Add(new Report
                    {
                        TitleName = "Торговая точка",
                        Address = data.Address,
                        State = data.Status == "В обработке" ? ReportStatus.inProcessing : ReportStatus.ready,
                        Description = data.Description
                    });
                }
            }
            //int i = 0;
            //while (i < 3)// Считываем все объекты и задаем для них соответсвующие свойства
            //{
            //    Reports.Add(new Report()
            //    {
            //        TitleName = i.ToString(),// Название товара\магазина
            //        Address = i.ToString() + "addr",//Адресс при наличии разумеется
            //        Number = i.ToString() + i.ToString(),//ИНН или что там в форме
            //        State = ReportStatus.inProcessing,// Статус заявки

            //        Description = "This is Description"//Описание которое появляется при нажатии на элемент
            //    });
            //    i++;
            //}
            //Reports.Add(new Report()
            //{
            //    TitleName = "Test without adress",
            //    Number = "77777",
            //    State = ReportStatus.ready,

            //    Description = "This is Description"
            //});
        }
    }
}
