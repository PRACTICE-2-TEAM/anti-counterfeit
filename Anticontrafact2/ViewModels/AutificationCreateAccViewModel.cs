using Anticontrafact2.Api;
using Anticontrafact2.Models;
using Anticontrafact2.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private async void CreateAcc()
        {
            // Валидация введенных значений
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(DoublePassword))
            {
                await page.DisplayAlert(null, "Заполните текстовые все поля", "Принять");
                return;
            }
            if (!Password.Equals(DoublePassword))
            {
                await page.DisplayAlert("", "Указанные вами пароли не совпадают", "Принять");
                return;
            }

            // Регистрация пользователя
            var api = AntiCounterfeitApiService.getInstance().Api;
            var signUpInfo = await api.RequestCode(UserName);
            if (!signUpInfo.Success)
            {
                await page.DisplayAlert("", signUpInfo.Reason, "Принять");
                return;
            }
            var code = await page.DisplayPromptAsync("", "Введите код подтверждения, отправленный на указанный вами адрес электронной почты");
            var registrationInfo = await api.SignUp(UserName, Password, code);
            if (!registrationInfo.Success)
            {
                await page.DisplayAlert("", registrationInfo.Reason, "Принять");
                return;
            }

            // Авторизация
            var logInInfo = await api.LogIn(UserName, Password);
            if (string.IsNullOrEmpty(logInInfo.Token))
            {
                await page.DisplayAlert("", "Пользователь с указанными данными не существует", "Принять");
                return;
            }
            User.GetUser().Email = UserName;
            User.GetUser().Token = logInInfo.Token;
            ToMainMenu();
        }

        private async void ToLoginPage()
        {
            await page.Navigation.PopAsync(false);
        }
        private void ToMainMenu()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
