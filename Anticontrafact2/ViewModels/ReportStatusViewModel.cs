using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    public class ReportStatusViewModel : BaseViewModel
    {
        /*
        0JXRgdC70Lgg0LLQsNGBINC30LDRgdGC0LDQstGP0YIg0Y3RgtC+INC/0L
        XRgNC10LTQtdC70YvQstCw0YLRjCAtINC90LUg0L/QtdGA0LXQtNC10LvR
        i9Cy0LDQudGC0LUsINC00LXQu9Cw0LnRgtC1INC/0L4g0L3QvtCy0L7QuS
        wg0YLQsNC6INC60LDQuiDRgtGD0YIg0LLRgdC1INC/0LvQvtGF0L4gPSg= 
        */
        ReportsStatusPage page;
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
            
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            Reports.Clear();

            var token = User.GetUser().Token;
            var identifiers = await api.GetComplaintIdentifiers(token, 100, 1);
            foreach (var identifier in identifiers)
            {
                var data = await api.GetComplaintData(token, identifier.Id);
                if (data.Type == "product")
                {
                    Reports.Add(new Report {
                        TitleName = "Товар",
                        Address = data.Address,
                        State = data.Status == "На рассмотрении" ? ReportStatus.inProcessing : ReportStatus.ready,
                        Description = data.Description
                    });
                }
                else if (data.Type == "sale-point")
                {
                    Reports.Add(new Report
                    {
                        TitleName = "Торговая точка",
                        Address = data.Address,
                        State = data.Status == "На рассмотрении" ? ReportStatus.inProcessing : ReportStatus.ready,
                        Description = data.Description
                    });
                }
            }

            IsBusy = false;
        }
    }
}
