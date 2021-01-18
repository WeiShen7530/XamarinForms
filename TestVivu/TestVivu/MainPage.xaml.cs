using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TestVivu.Views;

namespace TestVivu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LocationBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocationPage());
        }

        private void HotelBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllHotelPage());
        }
    }
}
