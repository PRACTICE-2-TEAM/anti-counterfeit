using Anticontrafact2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    public class ReportStatusViewModel:BaseViewModel
    {
        public ObservableCollection<Report> Reports { get; set; }
        public Command LoadReportsCommand { get; set; }
        public ReportStatusViewModel()
        {
            Reports = new ObservableCollection<Report>();

            LoadReportsCommand = new Command(LoadReports);
            LoadReports();
        }

        private void LoadReports()//TODO Тому кто будет соединять с API Нужно изменить только этот метод
        {
            
            int i = 0;
            while (i < 3)// Считываем все объекты и задаем для них соответсвующие свойства
            {
                Reports.Add(new Report()
                {
                    TitleName = i.ToString(),// Название товара\магазина
                    Address = i.ToString() + "addr",//Адресс при наличии разумеется
                    Number = i.ToString() + i.ToString(),//ИНН или что там в форме
                    State = ReportStatus.inProcessing,// Статус заявки

                    Description = "This is Description"//Описание которое появляется при нажатии на элемент
                }) ;
                i++;
            }
            Reports.Add(new Report()
            {
                TitleName = "Test without adress",
                Number = "77777",
                State = ReportStatus.ready,

                Description = "This is Description"
            });
        }
    }
}
