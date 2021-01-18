using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.Views;
using TestVivu.Helpers;

namespace TestVivu
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new RegisterPage());
            //MainPage = new AppShell();
            SetMainPage();
            //MainPage = new NavigationPage(new LoginPage());
        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainPage = new AppShell();
            }
            else if (!string.IsNullOrEmpty(Settings.Email) && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
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
