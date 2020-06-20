using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class AutificationCreateAccViewModel:BaseViewModel
    {
        AutificationCreateAccPage page;
        public AutificationCreateAccViewModel(AutificationCreateAccPage page)
        {
            this.page = page;
            CreateAccCommand = new Command(CreateAcc);
            ToLoginPageCommand = new Command(ToLoginPage);
        }
        public ICommand CreateAccCommand { get; }
        public ICommand ToLoginPageCommand { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DoublePassword { get; set; }

        private void CreateAcc()
        {
            // Зарегистрировать пользователя в БД
            // Можно сразуже выполнить вход
            //User.GetUser().Email = "Email@my.com";// Так задаем email. User - Singleton класс
        }
        private async void ToLoginPage()
        {
            await page.Navigation.PopAsync(false);
        }
    }
}
