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
            // shortcut
            IAntiCounterfeitApi Api = AntiCounterfeitApiService.getInstance().Api;

            // Проверяем все ли поля заполнены
            if (string.IsNullOrEmpty(UserName) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(DoublePassword))
            {
                await page.DisplayAlert("", "Заполните все поля", "OK");
                return;
            }
            // Проверяем совпадают ли пароли
            if (!Password.Equals(DoublePassword))
            {
                await page.DisplayAlert("", "Пароли не совпадают", "OK");
                return;
            }
            // Посылаем первый запрос для отправки на почту кода подтверждения
            SignUpInfo signUpInfo = await Api.SignUp(UserName);
            // Проверяем успешно ли прошла операция
            if (!signUpInfo.Success)
            {
                await page.DisplayAlert("", signUpInfo.Reason, "OK");
                return;
            }
            // Код пришел пользователю на почту, показываем диалоговое окно для ввода кода подтверждения
            string code = await page.DisplayPromptAsync("", "Код подтверждения");
            // Посылаем второй запрос на сервер для завершения регистрации
            RegistrationInfo registrationInfo = await Api.Register(UserName, Password, code);
            // Проверяем успешно ли прошла операция
            if (!registrationInfo.Success)
            {
                await page.DisplayAlert("", registrationInfo.Reason, "OK");
                return;
            }
            // Операция прошла успешно, пользователь зарегистрирован
            // Теперь авторизуем его
            LogInInfo logInInfo = await Api.LogIn(UserName, Password);
            string token = logInInfo.Token;
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

        private async void ToLoginPage()
        {
            await page.Navigation.PopAsync(false);
        }
    }
}
