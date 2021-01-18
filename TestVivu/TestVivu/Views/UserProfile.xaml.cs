using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Windows.Input;
using TestVivu.Helpers;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Settings.AccessToken = "";
                    Settings.Email = "";
                    Settings.Password = "";
                });
            }
        }

        private void editProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUserProfile());
        }

        private void LogoutBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}