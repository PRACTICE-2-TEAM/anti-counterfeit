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
        private async void LogIn()
        {
            // shortcut
            IAntiCounterfeitApi Api = AntiCounterfeitApiService.getInstance().Api;

            // Авторизуем пользователя
            LogInInfo logInInfo = await Api.LogIn(UserName, Password);
            string token = logInInfo.Token;
            await page.DisplayAlert("Debug",
                "UserName = " + UserName +
                ", Password = " + Password +
                ", token = " + token,
                "OK");
            // Проверяем ответ от сервера
            if (string.IsNullOrWhiteSpace(token))
            {
                await page.DisplayAlert("", "Пользователь с указанными данными не существует", "OK");
                return;
            }
            // Запоминаем адрес электронной почты и токен
            User.GetUser().Email = UserName;
            User.GetUser().Token = token;
        }
        private async void ToCreateAccPage()
        {
            await page.Navigation.PushAsync(new AutificationCreateAccPage(),false);
        }
    }
    
}
