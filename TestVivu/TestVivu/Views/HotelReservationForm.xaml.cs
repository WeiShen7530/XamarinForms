using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.Models;
using TestVivu.Services;
using System.Web;
using TestVivu.Helpers;
using System.Diagnostics;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelReservationForm : ContentPage
    {
        OrderInfo orderInfo;

        double originalPrice;

        ApiServices _apiServices = new ApiServices();

        public HotelReservationForm()
        {
            InitializeComponent();
        }

        public HotelReservationForm(Hotel hotel)
        {
            InitializeComponent();
            originalPrice = hotel.PRICE;
        }

        private void ReserveBtn_Clicked(object sender, EventArgs e)
        {
            string adults = pkrAdults.SelectedIndex.ToString();
            string children = pkrChildren.SelectedIndex.ToString();

            int adultsInt = Convert.ToInt32(adults);
            int childrenInt = Convert.ToInt32(children);

            //dateCheckIn.MinimumDate = DateTime.Today;
            //dateCheckOut.MinimumDate = DateTime.Today;

            orderInfo = new OrderInfo()
            {
                UserName = txtName.Text,
                UserEmail = txtEmail.Text,
                UserPhone = txtPhone.Text,
                UserAddress = txtAddress.Text,
                NumberOfAdults = adultsInt,
                NumberOfChildren = childrenInt,
                SpecialRequests = txtSpecialRequests.Text,
                CheckInDate = dateCheckIn.Date,
                CheckOutDate = dateCheckOut.Date
            };

            // adultsPrice
            double adultsPrice, adultsDouble;
            adultsDouble = Convert.ToDouble(adultsInt);
            
            if (adultsDouble == 1 || adultsDouble == 2)
            {
                adultsPrice = 0;
            }
            else
            {
                adultsPrice = originalPrice * adultsDouble / 2;
            }

            // childrenPrice
            double childrenPrice, childrenDouble;
            childrenDouble = Convert.ToDouble(childrenInt);

            if (childrenDouble == 1)
            {
                childrenPrice = 0;
            }
            else
            {
                childrenPrice = originalPrice * childrenDouble;
            }

            // roomPrederencePrice
            double roomPrederencePrice = 0;
            if (StandardRoom.IsChecked == true)
            {
                roomPrederencePrice = originalPrice;
                orderInfo.RoomPreference = "Chuẩn";
            }
            else if(DeluxeRoom.IsChecked == true)
            {
                roomPrederencePrice = originalPrice * 2;
                orderInfo.RoomPreference = "Sang trọng";
            }
            else if (SuiteRoom.IsChecked == true)
            {
                roomPrederencePrice = originalPrice * 3;
                orderInfo.RoomPreference = "Thượng hạng";
            }

            // daysPrice
            double daysPrice;
            TimeSpan timeSpan = orderInfo.CheckOutDate - orderInfo.CheckInDate;
            int numberOfDays = timeSpan.Days;

            if (numberOfDays == 1)
            {
                daysPrice = 0;
            }
            else
            {
                daysPrice = originalPrice * numberOfDays;
            }

            orderInfo.TotalMoney = daysPrice + roomPrederencePrice + adultsPrice + childrenPrice;

            //MoneyLbl.Text = orderInfo.TotalMoney.ToString() + ".000";

            //_apiServices.InsertHotelRoomReservationAsync(orderInfo, Settings.AccessToken);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Xác nhận đặt phòng!", "Tổng tiền: " + orderInfo.TotalMoney.ToString() + ".000", "Có", "không");

                if (result)
                {
                    await _apiServices.InsertHotelRoomReservationAsync(orderInfo, Settings.AccessToken);
                    await DisplayAlert("Thông báo", "Bạn đã đặt phòng thành công!","OK");
                    await Navigation.PushAsync(new ReserveInfoPage(orderInfo));
                }
            });
        }
    }
}