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

            AccauntAct = "Войти в аккаунт";
            EmailText = "Войдите в аккаунт";
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
        private async void LoginOrLogoutAcc()
        {
            
            if (User.GetUser().IsLogin)
            {
                User.GetUser().IsLogin = false;
                AccauntAct = "Войти в аккаунт";
                EmailText = "Войдите в аккаунт";



            }
            else//Крайне сомнительный способ
            {
                //AutificationLoginPage loginDialog = new AutificationLoginPage();
                NavigationPage loginDialog = new NavigationPage(new AutificationLoginPage());
                await page.Navigation.PushModalAsync(loginDialog);
                await Task.Run(()=>WaitCloseLoginDialog(loginDialog));
            }


            if(User.GetUser().IsLogin)
            {
                AccauntAct = "Выйти из аккаунта";
                EmailText = User.GetUser().Email;
            }
            page.ChangeShowReportsStatusButtonVisible();
            OnPropertyChanged(nameof(AccauntAct));
            OnPropertyChanged(nameof(EmailText));
        }
        private void WaitCloseLoginDialog(NavigationPage loginDialog)
        {
            while(true)
            {
                if(loginDialog.Navigation.ModalStack.Count==0)
                {
                    break;
                }
            }

        }
    }
}
