using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVivu.Helpers;
using TestVivu.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.ViewModels;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        

        private async void RegisterBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void GotoHomePage_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Email hoặc mật khẩu không hợp lệ. Vui lòng thử lại!", "OK");
            }
            
        }
    }
}