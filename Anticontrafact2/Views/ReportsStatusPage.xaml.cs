using Anticontrafact2.Models;
using Anticontrafact2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anticontrafact2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsStatusPage : ContentPage
    {
        public ReportsStatusPage()
        {
            InitializeComponent();

            BindingContext = new ReportStatusViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Report)layout.BindingContext;

            await DisplayAlert(item.Inform, item.Description, "Ok");
            
            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }
    }
}