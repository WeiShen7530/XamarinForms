using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace TestVivu.ViewModels
{
    public class AutoSlidingImageViewModel : BindableBase
    {
        private int _position;

        public int ImagePosition
        {
            get { return _position; }

            set
            {
                SetProperty(ref _position, value);
            }
        }

        public AutoSlidingImageViewModel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            {
                ImagePosition = (ImagePosition + 1) % 5;
                return true;
            }));
        }
    }
}
