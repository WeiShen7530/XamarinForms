using System;
using System.Collections.Generic;
using System.Text;

namespace TestVivu.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        private string _UserName;
        public string UserName
        {
            get { return this._UserName; }

            set
            {
                this._UserName = value;
                OnPropertyChanged();
            }
        }
    }
}
