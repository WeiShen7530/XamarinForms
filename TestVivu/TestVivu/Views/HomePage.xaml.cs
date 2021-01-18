using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.Models;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
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

        private void MainCarouselView_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            switch (MainCarouselView.Position)
            {
                case 0:
                    LtChooseBtn.Text = "Đà Lạt";
                    break;
                case 1:
                    LtChooseBtn.Text = "Vũng Tàu";
                    break;
                case 2:
                    LtChooseBtn.Text = "Phú Quốc";
                    break;
                case 3:
                    LtChooseBtn.Text = "Hà Nội";
                    break;
                case 4:
                    LtChooseBtn.Text = "Hồ Chí Minh";
                    break;
            }
        }

        private void LtChooseBtn_Clicked(object sender, EventArgs e)
        {
            switch (LtChooseBtn.Text)
            {
                case "Đà Lạt":
                    Navigation.PushAsync(new HotelPage(1));
                    break;
                case "Vũng Tàu":
                    Navigation.PushAsync(new HotelPage(2));
                    break;
                case "Phú Quốc":
                    Navigation.PushAsync(new HotelPage(3));
                    break;
                case "Hà Nội":
                    Navigation.PushAsync(new HotelPage(4));
                    break;
                case "Hồ Chí Minh":
                    Navigation.PushAsync(new HotelPage(5));
                    break;
            }
            
        }
    }
}