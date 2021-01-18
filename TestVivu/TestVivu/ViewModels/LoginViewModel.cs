using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TestVivu.Services;
using System.Windows.Input;
using TestVivu.Helpers;

namespace TestVivu.ViewModels
{
    public class LoginViewModel
    {
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin!", "OK");
                    }
                    var accessToken = await _apiServices.LoginAsync(Username, Password);

                    Settings.AccessToken = accessToken;
                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Email;
            Password = Settings.Password;
        }
    }
}
