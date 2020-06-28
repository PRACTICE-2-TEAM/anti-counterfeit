using Anticontrafact2.Api;
using Anticontrafact2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    public class ReportStatusViewModel : BaseViewModel
    {
        public ObservableCollection<Report> Reports { get; set; }
        public Command LoadReportsCommand { get; set; }
        public ReportStatusViewModel()
        {
            Reports = new ObservableCollection<Report>();

            LoadReportsCommand = new Command(async () => await LoadReports());
            LoadReportsCommand.Execute(null);
        }

        private async Task LoadReports()
        {
            IsBusy = true;
            Reports.Clear();

            string token = User.GetUser().Token;
            var api = AntiCounterfeitApiService.getInstance().Api;
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
            IsBusy = false;
        }
    }
}
