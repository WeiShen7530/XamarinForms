using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivuDuyKhang.Models
{
    public class OrderInfo
    {
        public int OrderID { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public string UserAddress { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public string RoomPreference { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public string SpecialRequests { get; set; }

        public double TotalMoney { get; set; }
    }
}
