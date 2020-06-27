using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Anticontrafact2.Services;
using Anticontrafact2.Views;

namespace Anticontrafact2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
            MainPage = new NavigationPage(new AutificationLoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
