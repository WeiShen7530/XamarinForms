using System;
using System.Collections.Generic;
using System.Text;

namespace TestVivu.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }

        public int LocationID { get; set; }

        public string HotelName { get; set; }

        public string HotelAddress { get; set; }

        public string HotelIntroduce { get; set; }

        public double PRICE { get; set; }

        public string HotelImage { get; set; }
    }
}
