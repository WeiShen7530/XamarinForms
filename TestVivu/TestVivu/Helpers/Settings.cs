using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using TestVivu.Helpers;

namespace TestVivu.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("Email", "");
            }

            set
            {
                AppSettings.AddOrUpdateValue("Email", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }
    }
}
