using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anticontrafact2.ViewModels
{
    class AutificftionLoginViewModel : BaseViewModel
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
        private async void LogIn()
        {
            // Проверяем доступно ли API
            if (!AntiCounterfeitApiService.getInstance().IsAvailable())
            {
                await page.DisplayAlert(null, "Нет подключения к сети", "Принять");
                return;
            }
            var api = AntiCounterfeitApiService.getInstance().Api;

            // Валидация полей
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                await page.DisplayAlert(null, "Заполните все текстовые поля", "Принять");
                return;
            }

            // Авторизация
            var logInInfo = await api.LogIn(UserName, Password);
            if (string.IsNullOrEmpty(logInInfo.Token))
            {
                await page.DisplayAlert(null, "Пользователь с указанными данными не существует", "Принять");
                return;
            }
            User.GetUser().Email = UserName;
            User.GetUser().Token = logInInfo.Token;
            ToMainMenu();
        }
        private async void ToCreateAccPage()
        {
            await page.Navigation.PushAsync(new AutificationCreateAccPage(), false);
        }
        private void ToMainMenu()
        {
            Application.Current.MainPage = new MainPage();
        }
    }

}
