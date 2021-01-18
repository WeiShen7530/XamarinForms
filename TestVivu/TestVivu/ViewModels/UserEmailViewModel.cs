using System;
using System.Collections.Generic;
using System.Text;
using TestVivu.Helpers;

namespace TestVivu.ViewModels
{
    class UserEmailViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public UserEmailViewModel()
        {
            Username = Settings.Email;
            Password = Settings.Password;
        }
    }
}
