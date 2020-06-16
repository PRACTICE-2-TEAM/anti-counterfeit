using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class AutificftionLoginViewModel:BaseViewModel
    {
        AutificationLoginPage page;
        public AutificftionLoginViewModel(AutificationLoginPage page)
        {
            this.page = page;

            LogInCommand = new Command(LogIn);
            ToCreateAccPageCommand = new Command(ToCreateAccPage);
        }

        public ICommand LogInCommand { get; }
        public ICommand ToCreateAccPageCommand { get; }

        public string Password { get; set; }
        public string UserName { get; set; }
        private void LogIn()
        {
            // Если норм пароль
            //Application.Current.MainPage
        }
        private async void ToCreateAccPage()
        {
            await page.Navigation.PushModalAsync(new AutificationCreateAccPage());
        }
    }
    
}
