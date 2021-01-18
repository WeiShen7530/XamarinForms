using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestVivu.Models;
using System.Dynamic;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using ModernHttpClient;
using System.Net.Mail;
using Xamarin.Forms;

namespace TestVivu.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            //if (!IsValidEmail(email))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Thông báo", "Email không hợp lệ. Vui lòng thử lại!", "OK");
            //    return false;
            //}
            //else
            //{
                var client = new HttpClient();

                var model = new RegisterBindingModel
                {
                    Email = email,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                var json = JsonConvert.SerializeObject(model);

                HttpContent content = new StringContent(json);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var respone = await client.PostAsync("http://tempwebapi.somee.com/api/Account/Register", content);

                return respone.IsSuccessStatusCode;
            //}
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            string URL = "http://tempwebapi.somee.com/Token";

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, URL);

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return accessToken;
        }

        public IList<OrderInfo> GetOrderInfoByIDAsync(string accessToken, int OrderInfoID)
        {
            IList<OrderInfo> data;
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var response = client.GetAsync("http://tempwebapi.somee.com/api/ServiceController/GetOrderInfo?orderID=" + OrderInfoID.ToString());
                var myString = response.GetAwaiter().GetResult();
                response.Wait();

                using (HttpContent content = myString.Content)
                {
                    var json = content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<OrderInfo>>(json.Result);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return data;
        }

        public async Task<bool> InsertHotelRoomReservationAsync(OrderInfo orderInfo, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(orderInfo);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://tempwebapi.somee.com/api/ServiceController/InsertHotelRoomReservation", content);

            return response.IsSuccessStatusCode;
        }

    }
}
