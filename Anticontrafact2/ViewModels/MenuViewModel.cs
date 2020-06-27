using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Anticontrafact2.ViewModels
{
    class MenuViewModel : BaseViewModel
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        MenuPage page;
        public MenuViewModel(MenuPage page)
        {

            this.page = page;
            LoginOrLogoutAccCommand = new Command(LoginOrLogoutAcc);
            ShowReportsStatusCommand = new Command(ShowReportsStatus);

            AccauntAct = "Выйти из аккаунта";
            EmailText = User.GetUser().Email;
        }

        public ICommand LoginOrLogoutAccCommand { get; }
        public ICommand ShowReportsStatusCommand { get; }

        public string AccauntAct { get; set; }
        public string EmailText { get; set; }

        private async void ShowReportsStatus()
        {
            await RootPage.NavigateFromMenu((int)MenuItemType.ReportsStatus);
            page.ResetSelectedItem();
        }
        private void LoginOrLogoutAcc()
        {
            Application.Current.MainPage = new NavigationPage(new AutificationLoginPage());
        }
    }
}
