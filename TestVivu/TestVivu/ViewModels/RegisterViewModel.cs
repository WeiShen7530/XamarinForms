using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TestVivu.Services;
using TestVivu.Helpers;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using TestVivu.Models;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestVivu.ViewModels
{
    public class RegisterViewModel
    {
        ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsUserExist(string email)
        {
            HttpClient client = new HttpClient();
            
            var result = await client.GetStringAsync
              ("http://tempwebapi.somee.com/api/ServiceController/GetUsers");
            
            var userLists = JsonConvert.DeserializeObject<List<UserEmailModel>>(result);
            Debug.WriteLine(userLists);

            for (int i = 0; i < userLists.Count; i++)
            {
                Debug.WriteLine(userLists[i].Email);
                Debug.WriteLine(userLists[i]);
                if (email == userLists[i].Email)
                {
                    return true;
                }

                //UserEmailModel
            }

            return false;
        }

        public Regex rx = new Regex(@"(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,}");

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!IsValidEmail(Email))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Email không hợp lệ. Vui lòng thử lại!", "OK");
                    }
                    else if (await IsUserExist(Email))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Tài khoản đã tồn tại. Vui lòng thử Email khác!", "OK");
                    }
                    else if (string.IsNullOrEmpty(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập mật khẩu!", "OK");
                    }
                    else if (string.IsNullOrEmpty(ConfirmPassword))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng xác nhận mật khẩu!", "OK");
                    }
                    else if (Password != ConfirmPassword)
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Xác nhận mật khẩu không trùng khớp. Vui lòng nhập lại!", "OK");
                    }
                    else if (!rx.IsMatch(Password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu phải có ít nhất 8 ký tự. Bao gồm 1 ký tự viết hoa, 1 ký tự đặc biệt và 1 chữ số!", "OK");
                    }
                    else
                    {
                        var isSuccess = await _apiServices.RegisterAsync(Email, Password, ConfirmPassword);

                        Settings.Email = Email;
                        Settings.Password = Password;

                        if (isSuccess)
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký thành công!", "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký thất bại. Vui lòng thử lại!", "OK");
                        }
                    }
                });
            }
        }
    }
}
