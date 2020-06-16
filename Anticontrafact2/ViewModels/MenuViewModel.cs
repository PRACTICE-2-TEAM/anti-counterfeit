using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class MenuViewModel : BaseViewModel
    {
        MenuPage page;
        MainPage RootPage;
        public MenuViewModel(MenuPage page)
        {

            this.page = page;
            RootPage = Application.Current.MainPage as MainPage;
            LoginOrLogoutAccCommand = new Command(LoginOrLogoutAcc);
        }

        public ICommand LoginOrLogoutAccCommand { get; }

        private async void LoginOrLogoutAcc()
        {
            //if(RootPage.User.IsLogin)
            if(false)
            {
                page.accauntAct = "Выйти из акаунта";
                RootPage.User.Email = null;
            }
            else
            {
                await page.Navigation.PushModalAsync(new NavigationPage(new AutificationLoginPage()));
            }
        }
    }
}
