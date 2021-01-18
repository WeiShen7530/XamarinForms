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
    public partial class LocationPage : ContentPage
    {
        public List<Location> locationsList;

        public LocationPage()
        {
            InitializeComponent();
            InitLocationList();
        }
        //public void InitLocationList()
        //{
        //    locationsList = new List<Location>();
        //    locationsList.Add(new Location { LocationName = "Đà Lạt", Image = "DaLat.jpg" });
        //    locationsList.Add(new Location { LocationName = "Vũng Tàu", Image = "VungTau.jpg" });
        //    locationsList.Add(new Location { LocationName = "Phú Quốc", Image = "PhuQuoc.jpg" });
        //    locationsList.Add(new Location { LocationName = "Hà Nội", Image = "HaNoi.jpg" });
        //    locationsList.Add(new Location { LocationName = "Hồ Chí Minh", Image = "HCM.jpg" });

        //    LstLocation.ItemsSource = locationsList;
        //}

        private async void InitLocationList()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
              ("http://tempwebapi.somee.com/api/ServiceController/GetAllLocation");
            locationsList = JsonConvert.DeserializeObject<List<Location>>(kq);
            LstLocation.ItemsSource = locationsList;
        }

        private void LstLocation_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LstLocation.SelectedItem != null)
            {
                Location location = (Location)LstLocation.SelectedItem;

                Navigation.PushAsync(new HotelPage(location));
            }
        }

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        private void SearchLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = locationsList.Where(lt => RemoveSign4VietnameseString(lt.LocationName).ToLower().Contains(SearchLocation.Text.ToLower()));
            LstLocation.ItemsSource = searchResult;
        }
    }
}