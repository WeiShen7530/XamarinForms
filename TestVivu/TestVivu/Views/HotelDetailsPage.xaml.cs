using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDetailsPage : ContentPage
    {
        Hotel htDetails;
        public HotelDetailsPage()
        {
            InitializeComponent();
        }

        public HotelDetailsPage(Hotel hotel)
        {
            InitializeComponent();
            Title = hotel.HotelName;
            InitializeHotelDetail(hotel);
            htDetails = hotel;
        }

        private async void InitializeHotelDetail(Hotel ht)
        {
            HttpClient http = new HttpClient();

            var kq = await http.GetStringAsync
                ("http://tempwebapi.somee.com/api/ServiceController/GetHotelDetails?htID=" + ht.HotelID.ToString());

            var myDeserializedHotelDetails = JsonConvert.DeserializeObject<List<Hotel>>(kq);

            Hotel hotelDetails = myDeserializedHotelDetails[0];
            //ht = hotelDetails;

            //Title = ht.HotelName;
            Title = hotelDetails.HotelName;

            htImg.Source = hotelDetails.HotelImage;
            htName.Text = hotelDetails.HotelName;
            htAddress.Text = hotelDetails.HotelAddress;
            htPrice.Text = "Giá: VND " + hotelDetails.PRICE.ToString() + ".000";
            htIntro.Text = hotelDetails.HotelIntroduce;
        }

        private void ReserveBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HotelReservationForm(htDetails));
        }
    }
}