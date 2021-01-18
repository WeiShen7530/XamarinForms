using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestVivu.Models;
using System.Diagnostics;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReserveInfoPage : ContentPage
    {
        OrderInfo orderInfomation;
        public ReserveInfoPage()
        {
            InitializeComponent();
            InitializeLastestOrderInfo();
        }

        public ReserveInfoPage(OrderInfo orderInfo)
        {
            InitializeComponent();
            InitializeReserveInfo(orderInfo);
            orderInfomation = orderInfo;
        }

        private async void InitializeReserveInfo(OrderInfo oi)
        {
            HttpClient http = new HttpClient();

            var kq = await http.GetStringAsync
                ("http://tempwebapi.somee.com/api/ServiceController/GetOrderInfoByOtherFields?userName=" + oi.UserName + "&userEmail=" + oi.UserEmail + "&userPhone=" + oi.UserPhone + "&checkInDate=" + oi.CheckInDate.ToString() + "&checkOutDate=" + oi.CheckOutDate.ToString());

            var myDeserializedOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(kq);

            OrderInfo finalOrderInfo = myDeserializedOrderInfo[0];

            txtName.Text = finalOrderInfo.UserName;
            txtEmail.Text = finalOrderInfo.UserEmail;
            txtPhone.Text = finalOrderInfo.UserPhone;
            txtAddress.Text = finalOrderInfo.UserAddress;

            adultsLbl.Text = finalOrderInfo.NumberOfAdults.ToString();
            childrenLbl.Text = finalOrderInfo.NumberOfChildren.ToString();

            RoomType.Text = finalOrderInfo.RoomPreference;
            txtSpecialRequests.Text = finalOrderInfo.SpecialRequests;
            MoneyLbl.Text = finalOrderInfo.TotalMoney.ToString() + ".000";

            dateCheckIn.Text = finalOrderInfo.CheckInDate.Date.ToString();
            dateCheckOut.Text = finalOrderInfo.CheckOutDate.Date.ToString();
            
        }

        private async void InitializeLastestOrderInfo()
        {
            HttpClient http = new HttpClient();

            var kq = await http.GetStringAsync
                ("http://tempwebapi.somee.com/api/ServiceController/GetLastestOrder");

            var myDeserializedOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(kq);

            OrderInfo finalOrderInfo = myDeserializedOrderInfo[0];

            txtName.Text = finalOrderInfo.UserName;
            txtEmail.Text = finalOrderInfo.UserEmail;
            txtPhone.Text = finalOrderInfo.UserPhone;
            txtAddress.Text = finalOrderInfo.UserAddress;

            adultsLbl.Text = finalOrderInfo.NumberOfAdults.ToString();
            childrenLbl.Text = finalOrderInfo.NumberOfChildren.ToString();

            RoomType.Text = finalOrderInfo.RoomPreference;
            txtSpecialRequests.Text = finalOrderInfo.SpecialRequests;
            MoneyLbl.Text = finalOrderInfo.TotalMoney.ToString() + ".000";

            dateCheckIn.Text = finalOrderInfo.CheckInDate.Date.ToString();
            dateCheckOut.Text = finalOrderInfo.CheckOutDate.Date.ToString();
        }

        private void GotoHomePage_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new AppShell());
            Application.Current.MainPage = new AppShell();
        }
    }
}