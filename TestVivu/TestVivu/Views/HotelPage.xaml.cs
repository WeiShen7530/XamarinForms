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
using System.Diagnostics;

namespace TestVivu.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelPage : ContentPage
    {
        public List<Hotel> hotelList;
        public HotelPage()
        {
            InitializeComponent();
        }

        public HotelPage(Location location)
        {
            InitializeComponent();
            Title = location.LocationName;
            InitilizeHotel(location);
        }

        public HotelPage(int locationID)
        {
            InitializeComponent();
            InitilizeHotelByLocationID(locationID);
        }

        //void InitilizeHotel(Location ltc)
        //{
        //    List<Hotel> hotelsList = new List<Hotel>();

        //    // Da Lat.
        //    if (ltc.LocationName == "Đà Lạt")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Hotel Colline",
        //            Address = "10 Phan Boi Chau Street, Ward 1, Da Lat",
        //            Introduce = "Nằm ở trung tâm thành phố Đà Lạt, cách Quảng trường Lâm Viên 500m, Hotel Colline có quầy bar và các phòng với truy cập Wi-Fi miễn phí." +
        //            " Khách sạn này nằm cách Hồ Xuân Hương 200 m và cách công viên Yersin 500m.",
        //            Image = "Hotel_Colline.webp",
        //            Price = 1765
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Terracotta Hotel & Resort Dalat",
        //            Address = "Zone 7.9, Tuyen Lam Lake Tourist Area, Ward 3, Tuyen Lam Lake, Da Lat, Viet Nam",
        //            Introduce = "Terracotta Hotel & Resort Dalat cung cấp các phòng và biệt thự với Wi-Fi miễn phí." +
        //            " Resort nằm cạnh Hồ Tuyền Lâm đồng thời có hồ bơi trong nhà, trung tâm thể dục và spa.",
        //            Image = "Terracotta_HotelResort.jpg",
        //            Price = 1228
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Ana Villas Dalat Resort & Spa",
        //            Address = "Le Lai Street, Ward 5, Da Lat, Viet Nam",
        //            Introduce = "Ana Villas Dalat Resort & Spa cung cấp các biệt thự kiểu thuộc địa Pháp nguyên bản tại một vị trí lý tưởng trên những sườn dốc của vùng cao nguyên nông thôn ở thành phố Đà Lạt." +
        //            " Resort này có hồ bơi ngoài trời, trung tâm spa và nhà hàng trong khuôn viên. Du khách có thể sử dụng WiFi miễn phí ở các khu vực chung và chỗ đỗ xe riêng miễn phí trong khuôn viên.",
        //            Image = "Ana_Villas_Dalat_Resort_Spa.webp",
        //            Price = 1816
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Kings Hotel Dalat",
        //            Address = "10 Bui Thi Xuan, Da Lat, Viet Nam",
        //            Introduce = "Khách sạn Kings Hotel Dalat nằm tại thành phố Đà Lạt này có tiện nghi BBQ và trung tâm spa." +
        //            " Khách sạn có phòng xông hơi khô và trung tâm thể dục đồng thời khách có thể dùng bữa trong nhà hàng. WiFi trong toàn bộ khuôn viên và chỗ đỗ xe riêng tại chỗ đều được cung cấp miễn phí.",
        //            Image = "Kings_Hotel_Dalat.jpg",
        //            Price = 758
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Dalat Palace Heritage Hotel",
        //            Address = "02 Tran Phu Street, Ward 3, Da Lat, Viet Nam",
        //            Introduce = "Dalat Palace Hotel tọa lạc tại trung tâm thành phố Đà Lạt, cách Dinh Bảo Đại chưa đến 300 m." +
        //            " Tọa lạc trong một công viên tư nhân, khách sạn thông gió tự nhiên này có nhà hàng, spa và Wi-Fi miễn phí.",
        //            Image = "Dalat_Palace_Heritage_Hotel.webp",
        //            Price = 3483
        //        });
        //    }

        //    // Vung Tau.
        //    else if (ltc.LocationName == "Vũng Tàu")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "The Imperial Hotel Vung Tau",
        //            Address = "159 Thuy Van Street, Vung Tau, Viet Nam",
        //            Introduce = "The Imperial Hotel Vung Tau có khu vực bãi biển riêng và cung cấp chỗ nghỉ với lối trang trí thời Victoria ở khu vực Bãi Sau." +
        //            " Khách sạn có hồ bơi ngoài trời và 4 địa điểm ăn uống. Khách có thể sử dụng Wi-Fi miễn phí trong toàn khuôn viên.",
        //            Image = "The_Imperial_Hotel.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Pullman Vung Tau",
        //            Address = "15 Thi Sach, Thang Tam, Vung Tau, Viet Nam",
        //            Introduce = "Nằm ở thành phố Vũng Tàu, cách Bãi Sau 450 m, Pullman Vung Tau cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, hồ bơi ngoài trời và trung tâm thể dục." +
        //            " Trong số nhiều tiện nghi của khách sạn này còn có quán bar, khu vườn và khu vực bãi biển riêng. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Pullman_VungTau.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Fusion Suites Vung Tau",
        //            Address = "02 Truong Cong Dinh Street, Ward 2, Vung Tau City, BR-VT, Viet Nam",
        //            Introduce = "Nằm ở thành phố Vũng Tàu, cách Bãi Sau 1,8 km, Fusion Suites Vung Tau cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, trung tâm thể dục và quầy bar." +
        //            " Khách sạn 4 sao này cũng cung cấp khu vườn, sân hiên, Chỗ nghỉ có lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Fusion_Suites_VungTau.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Marina Bay Vung Tau Resort & Spa",
        //            Address = "115 Tran Phu, Vung Tau, Viet Nam",
        //            Introduce = "Marina Bay Vung Tau Resort & Spa có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quán bar ở thành phố Vũng Tàu." +
        //            " Các tiện nghi của chỗ nghỉ bao gồm dịch vụ lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Resort còn có vườn và sân hiên tắm nắng.",
        //            Image = "Marina_Bay_VungTau_Resort_Spa.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Malibu Hotel",
        //            Address = "263 Le Hong Phong, Vung Tau, Viet Nam",
        //            Introduce = "Tọa lạc trong một tòa nhà chọc trời màu xám, hiện đại và nổi bật, Malibu Hotel cung cấp chỗ nghỉ đẹp mắt tại thành phố Vũng Tàu." +
        //            " Khách sạn có hồ bơi vô cực thú vị nhìn ra khu vực Vũng Tàu. Khách nghỉ tại đây có thể lựa chọn thư giãn đôi chút tại spa hoặc thậm chí rèn luyện sức khỏe ở trung tâm thể dục. Bạn đồng hành tuyệt vời và cocktail hảo hạng đang chờ đón du khách tại sảnh khách cũng như các quầy bar ngay trong khuôn viên. Wi-Fi miễn phí có trong tất cả các khu vực của chỗ nghỉ.",
        //            Image = "Malibu_Hotel.webp"
        //        });
        //    }

        //    // Phu Quoc.
        //    else if (ltc.LocationName == "Phú Quốc")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sunset Beach Resort and Spa",
        //            Address = "100/2 Tran Hung Dao Street, Duong To, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Tọa lạc tại đảo Phú Quốc, cách Chùa Sùng Hưng 1,9 km, Sunset Beach Resort and Spa có nhà hàng, chỗ đỗ xe riêng miễn phí, hồ bơi ngoài trời và trung tâm thể dục." +
        //            " Các tiện nghi của chỗ nghỉ bao gồm dịch vụ lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cũng có hồ bơi trong nhà và phòng giữ hành lý.",
        //            Image = "Sunset_Beach_Resort_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Movenpick Residences Phu Quoc",
        //            Address = "Ong Lang Beach, Cua Duong Village, Ong Lang, 92000 Phu Quoc, Viet Nam",
        //            Introduce = "Tọa lạc tại đảo Phú Quốc, Mövenpick Residences Phu Quoc có nhà hàng, hồ bơi ngoài trời, quán bar và sảnh khách chung." +
        //            " Mỗi phòng nghỉ tại khách sạn 5 sao này đều có tầm nhìn ra hồ nước, khu vườn và khu vực bãi biển riêng. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Movenpick_Residences_PhuQuoc.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sen Hotel Phu Quoc",
        //            Address = "63 Tran Hung Dao Street, Phu Quoc, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Sen Hotel Phu Quoc tọa lạc bên bờ biển ở đảo Phú Quốc, cách Chùa Sùng Hưng 500 m và Sòng bạc Corona 22 km." +
        //            " Trong số các tiện nghi của chỗ nghỉ này có nhà hàng, lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cung cấp phòng gia đình.",
        //            Image = "Sen_Hotel_PhuQuoc.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Seashells Phu Quoc Hotel & Spa",
        //            Address = "1 Vo Thi Sau, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Seashells Phu Quoc Hotel & Spa tọa lạc ở thị trấn Dương Đông, đảo Phú Quốc và sở hữu các phòng nghỉ hiện đại với kết nối WiFi miễn phí." +
        //            " Khách sạn này có hồ bơi ngoài trời cùng nhà hàng, quán bar và chỗ đỗ xe riêng miễn phí trong khuôn viên.",
        //            Image = "Seashells_PhuQuoc_Hotel_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Fusion Resort Phu Quoc - All Spa Inclusive",
        //            Address = "Vung Bau, Cua Can, Phu Quoc, Viet Nam",
        //            Introduce = "Cung cấp chỗ ở ấm cúng tại Cửa Cạn, Fusion Resort Phu Quoc có hồ bơi ngoài trời, nhà hàng trong khuôn viên và khu vườn." +
        //            " Khách được cung cấp dịch vụ đưa đón sân bay miễn phí với lịch trình cố định và truy cập Wi-Fi miễn phí trong toàn bộ khu vực.",
        //            Image = "Fusion_Resort_PhuQuoc.jpg"
        //        });
        //    }

        //    // Ha Noi.
        //    else if (ltc.LocationName == "Hà Nội")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Peridot Grand Hotel & Spa by AIRA",
        //            Address = "33 Duong Thanh, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, Peridot Grand Hotel & Spa by AIRA có 2 nhà hàng trong khuôn viên, 3 quán bar, hồ bơi ngoài trời, trung tâm thể dục và quán bar." +
        //            " Khách sạn 5 sao này cũng có sảnh khách chung và dịch vụ concierge. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Peridot_Grand_Hotel_Spa_by_AIRA.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Solaria Hanoi Hotel",
        //            Address = "22 Bao Khanh, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc ở thành phố Hà Nội, cách Nhà Thờ Lớn 300 m, Solaria Hanoi Hotel có quầy bar, sân hiên và tầm nhìn ra quang cảnh thành phố." +
        //            " Trong số các tiện nghi của khách sạn này còn có nhà hàng, lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cung cấp các phòng gia đình.",
        //            Image = "Solaria_Hanoi_Hotel.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Acoustic Hotel & Spa",
        //            Address = "39 Tho Nhuom, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, Acoustic Hotel & Spa có nhà hàng, xe đạp cho khách sử dụng miễn phí, trung tâm thể dục và quán bar." +
        //            " Khách sạn này có các phòng gia đình và sân hiên. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Acoustic_Hotel_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sofitel Legend Metropole Hanoi",
        //            Address = "15 Ngo Quyen Street, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Là địa danh lịch sử sang trọng từ năm 1901, Sofitel Legend Metropole có các dịch vụ spa thư giãn, dịch vụ phòng 24 giờ và hồ bơi nước nóng." +
        //            " Khách sạn tọa lạc ở trung tâm thủ đô Hà Nội, gần Khu Phố Cổ.",
        //            Image = "Sofitel_Legend_Metropole_Hanoi.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Hanoi Paradise Center Hotel & Spa",
        //            Address = "22/5 Hang Voi Street, Ly Thai To Ward, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, cách Nhà hát múa rối nước Thăng Long 400 m, Hanoi Paradise Center Hotel & Spa cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, quán bar và sảnh khách chung." +
        //            " Khách sạn này có các phòng gia đình và sân hiên. Chỗ nghỉ cũng cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Hanoi_Paradise_Center_Hotel_Spa.webp"
        //        });
        //    }

        //    // Ho Chi Minh.
        //    else if (ltc.LocationName == "Hồ Chí Minh")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Winsuites Saigon",
        //            Address = "28-30-32 Le Lai Street, Ben Thanh Ward, District 1, HCM City, Viet Nam",
        //            Introduce = "Tọa lạc tại Thành phố Hồ Chí Minh, Winsuites Saigon có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quán bar." +
        //            " Khách sạn 4 sao này còn có dịch vụ tiền sảnh và bàn đặt tour. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ đưa đón sân bay, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên.",
        //            Image = "Winsuites_Saigon.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Icon Saigon - LifeStyle Design Hotel",
        //            Address = "65-67 Hai Ba Trung, District 1, HCM City, Viet Nam",
        //            Introduce = "Tọa lạc ở Thành phố Hồ Chí Minh, Icon Saigon - LifeStyle Hotel có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quầy bar." +
        //            " Khách sạn 4 sao này cung cấp WiFi miễn phí, dịch vụ lễ tân 24 giờ và dịch vụ phòng. Chỗ nghỉ nằm gần các điểm tham quan nổi tiếng như Trụ sở UBND Thành phố Hồ Chí Minh, Bưu điện Trung tâm Sài Gòn và Nhà thờ Đức Bà.",
        //            Image = "Icon_Saigon_LifeStyle_Design_Hotel.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "The Myst Dong Khoi",
        //            Address = "6-8 Ho Huan Nghiep, District 1, HCM City, Viet Nam",
        //            Introduce = "Với thiết kế và nội thất hiện đại cổ điển, The Myst Dong Khoi cung cấp chỗ nghỉ tinh tế ở khu trung tâm Quận 1 tại Thành phố Hồ Chí Minh." +
        //            " Du khách có thể ngâm mình thư giãn trong hồ bơi trên sân thượng, hoặc đơn giản là ngắm nhìn thành phố bên sông Sài Gòn từ sàn hồ bơi.",
        //            Image = "The_Myst_Dong_Khoi.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Grand Hotel Saigon",
        //            Address = "8 Dong Khoi Street, District 1, HCM City, Viet Nam",
        //            Introduce = "Nằm trong toà nhà kiểu thuộc địa được khôi phục lại, Grand Hotel Saigon cung cấp chỗ nghỉ rộng rãi tại Quận 1, thành phố Hồ Chí Minh." +
        //            " Khách sạn có hồ bơi ngoài trời, 2 nhà hàng trong khuôn viên và WiFi miễn phí.",
        //            Image = "Grand_Hotel_Saigon.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Caravelle Saigon",
        //            Address = "19-23 Lam Son Square, District 1, HCM City, Viet Nam",
        //            Introduce = "Khai trương vào năm 1959, khách sạn 5 sao Caravelle Saigon có phong cách kiến trúc kiểu Pháp và Việt Nam đẹp mắt, hồ bơi ngoài trời cùng phòng không hút thuốc đi kèm Wi-Fi miễn phí." +
        //            " Nằm trong bán kính chỉ 50 m từ Nhà hát thành phố nổi tiếng ở Thành phố Hồ Chí Minh, khách sạn thân thiện với môi trường này cũng có quán bar trên sân thượng biểu diễn nhạc sống.",
        //            Image = "Caravelle_Saigon.jpg"
        //        });
        //    }

        //    LstHotel.ItemsSource = hotelsList;
        //}
        //}

        //void InitilizeHotelByName(string locationName)
        //{
        //    List<Hotel> hotelsList = new List<Hotel>();

        //    // Da Lat.
        //    if (locationName == "Đà Lạt")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Hotel Colline",
        //            Address = "10 Phan Boi Chau Street, Ward 1, Da Lat",
        //            Introduce = "Nằm ở trung tâm thành phố Đà Lạt, cách Quảng trường Lâm Viên 500m, Hotel Colline có quầy bar và các phòng với truy cập Wi-Fi miễn phí." +
        //            " Khách sạn này nằm cách Hồ Xuân Hương 200 m và cách công viên Yersin 500m.",
        //            Image = "Hotel_Colline.webp",
        //            Price = 1000.000
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Terracotta Hotel & Resort Dalat",
        //            Address = "Zone 7.9, Tuyen Lam Lake Tourist Area, Ward 3, Tuyen Lam Lake, Da Lat, Viet Nam",
        //            Introduce = "Terracotta Hotel & Resort Dalat cung cấp các phòng và biệt thự với Wi-Fi miễn phí." +
        //            " Resort nằm cạnh Hồ Tuyền Lâm đồng thời có hồ bơi trong nhà, trung tâm thể dục và spa.",
        //            Image = "Terracotta_HotelResort.jpg",
        //            Price = 1500.000
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Ana Villas Dalat Resort & Spa",
        //            Address = "Le Lai Street, Ward 5, Da Lat, Viet Nam",
        //            Introduce = "Ana Villas Dalat Resort & Spa cung cấp các biệt thự kiểu thuộc địa Pháp nguyên bản tại một vị trí lý tưởng trên những sườn dốc của vùng cao nguyên nông thôn ở thành phố Đà Lạt." +
        //            " Resort này có hồ bơi ngoài trời, trung tâm spa và nhà hàng trong khuôn viên. Du khách có thể sử dụng WiFi miễn phí ở các khu vực chung và chỗ đỗ xe riêng miễn phí trong khuôn viên.",
        //            Image = "Ana_Villas_Dalat_Resort_Spa.webp",
        //            Price = 2000.000
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Kings Hotel Dalat",
        //            Address = "10 Bui Thi Xuan, Da Lat, Viet Nam",
        //            Introduce = "Khách sạn Kings Hotel Dalat nằm tại thành phố Đà Lạt này có tiện nghi BBQ và trung tâm spa." +
        //            " Khách sạn có phòng xông hơi khô và trung tâm thể dục đồng thời khách có thể dùng bữa trong nhà hàng. WiFi trong toàn bộ khuôn viên và chỗ đỗ xe riêng tại chỗ đều được cung cấp miễn phí.",
        //            Image = "Kings_Hotel_Dalat.jpg",
        //            Price = 2500.000
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Dalat Palace Heritage Hotel",
        //            Address = "02 Tran Phu Street, Ward 3, Da Lat, Viet Nam",
        //            Introduce = "Dalat Palace Hotel tọa lạc tại trung tâm thành phố Đà Lạt, cách Dinh Bảo Đại chưa đến 300 m." +
        //            " Tọa lạc trong một công viên tư nhân, khách sạn thông gió tự nhiên này có nhà hàng, spa và Wi-Fi miễn phí.",
        //            Image = "Dalat_Palace_Heritage_Hotel.webp",
        //            Price = 3000.000
        //        });
        //    }

        //    // Vung Tau.
        //    else if (locationName == "Vũng Tàu")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "The Imperial Hotel Vung Tau",
        //            Address = "159 Thuy Van Street, Vung Tau, Viet Nam",
        //            Introduce = "The Imperial Hotel Vung Tau có khu vực bãi biển riêng và cung cấp chỗ nghỉ với lối trang trí thời Victoria ở khu vực Bãi Sau." +
        //            " Khách sạn có hồ bơi ngoài trời và 4 địa điểm ăn uống. Khách có thể sử dụng Wi-Fi miễn phí trong toàn khuôn viên.",
        //            Image = "The_Imperial_Hotel.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Pullman Vung Tau",
        //            Address = "15 Thi Sach, Thang Tam, Vung Tau, Viet Nam",
        //            Introduce = "Nằm ở thành phố Vũng Tàu, cách Bãi Sau 450 m, Pullman Vung Tau cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, hồ bơi ngoài trời và trung tâm thể dục." +
        //            " Trong số nhiều tiện nghi của khách sạn này còn có quán bar, khu vườn và khu vực bãi biển riêng. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Pullman_VungTau.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Fusion Suites Vung Tau",
        //            Address = "02 Truong Cong Dinh Street, Ward 2, Vung Tau City, BR-VT, Viet Nam",
        //            Introduce = "Nằm ở thành phố Vũng Tàu, cách Bãi Sau 1,8 km, Fusion Suites Vung Tau cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, trung tâm thể dục và quầy bar." +
        //            " Khách sạn 4 sao này cũng cung cấp khu vườn, sân hiên, Chỗ nghỉ có lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Fusion_Suites_VungTau.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Marina Bay Vung Tau Resort & Spa",
        //            Address = "115 Tran Phu, Vung Tau, Viet Nam",
        //            Introduce = "Marina Bay Vung Tau Resort & Spa có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quán bar ở thành phố Vũng Tàu." +
        //            " Các tiện nghi của chỗ nghỉ bao gồm dịch vụ lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Resort còn có vườn và sân hiên tắm nắng.",
        //            Image = "Marina_Bay_VungTau_Resort_Spa.webp"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Malibu Hotel",
        //            Address = "263 Le Hong Phong, Vung Tau, Viet Nam",
        //            Introduce = "Tọa lạc trong một tòa nhà chọc trời màu xám, hiện đại và nổi bật, Malibu Hotel cung cấp chỗ nghỉ đẹp mắt tại thành phố Vũng Tàu." +
        //            " Khách sạn có hồ bơi vô cực thú vị nhìn ra khu vực Vũng Tàu. Khách nghỉ tại đây có thể lựa chọn thư giãn đôi chút tại spa hoặc thậm chí rèn luyện sức khỏe ở trung tâm thể dục. Bạn đồng hành tuyệt vời và cocktail hảo hạng đang chờ đón du khách tại sảnh khách cũng như các quầy bar ngay trong khuôn viên. Wi-Fi miễn phí có trong tất cả các khu vực của chỗ nghỉ.",
        //            Image = "Malibu_Hotel.webp"
        //        });
        //    }

        //    // Phu Quoc.
        //    else if (locationName == "Phú Quốc")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sunset Beach Resort and Spa",
        //            Address = "100/2 Tran Hung Dao Street, Duong To, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Tọa lạc tại đảo Phú Quốc, cách Chùa Sùng Hưng 1,9 km, Sunset Beach Resort and Spa có nhà hàng, chỗ đỗ xe riêng miễn phí, hồ bơi ngoài trời và trung tâm thể dục." +
        //            " Các tiện nghi của chỗ nghỉ bao gồm dịch vụ lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cũng có hồ bơi trong nhà và phòng giữ hành lý.",
        //            Image = "Sunset_Beach_Resort_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Movenpick Residences Phu Quoc",
        //            Address = "Ong Lang Beach, Cua Duong Village, Ong Lang, 92000 Phu Quoc, Viet Nam",
        //            Introduce = "Tọa lạc tại đảo Phú Quốc, Mövenpick Residences Phu Quoc có nhà hàng, hồ bơi ngoài trời, quán bar và sảnh khách chung." +
        //            " Mỗi phòng nghỉ tại khách sạn 5 sao này đều có tầm nhìn ra hồ nước, khu vườn và khu vực bãi biển riêng. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Movenpick_Residences_PhuQuoc.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sen Hotel Phu Quoc",
        //            Address = "63 Tran Hung Dao Street, Phu Quoc, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Sen Hotel Phu Quoc tọa lạc bên bờ biển ở đảo Phú Quốc, cách Chùa Sùng Hưng 500 m và Sòng bạc Corona 22 km." +
        //            " Trong số các tiện nghi của chỗ nghỉ này có nhà hàng, lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cung cấp phòng gia đình.",
        //            Image = "Sen_Hotel_PhuQuoc.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Seashells Phu Quoc Hotel & Spa",
        //            Address = "1 Vo Thi Sau, Duong Dong, Phu Quoc, Viet Nam",
        //            Introduce = "Seashells Phu Quoc Hotel & Spa tọa lạc ở thị trấn Dương Đông, đảo Phú Quốc và sở hữu các phòng nghỉ hiện đại với kết nối WiFi miễn phí." +
        //            " Khách sạn này có hồ bơi ngoài trời cùng nhà hàng, quán bar và chỗ đỗ xe riêng miễn phí trong khuôn viên.",
        //            Image = "Seashells_PhuQuoc_Hotel_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Fusion Resort Phu Quoc - All Spa Inclusive",
        //            Address = "Vung Bau, Cua Can, Phu Quoc, Viet Nam",
        //            Introduce = "Cung cấp chỗ ở ấm cúng tại Cửa Cạn, Fusion Resort Phu Quoc có hồ bơi ngoài trời, nhà hàng trong khuôn viên và khu vườn." +
        //            " Khách được cung cấp dịch vụ đưa đón sân bay miễn phí với lịch trình cố định và truy cập Wi-Fi miễn phí trong toàn bộ khu vực.",
        //            Image = "Fusion_Resort_PhuQuoc.jpg"
        //        });
        //    }

        //    // Ha Noi.
        //    else if (locationName == "Hà Nội")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Peridot Grand Hotel & Spa by AIRA",
        //            Address = "33 Duong Thanh, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, Peridot Grand Hotel & Spa by AIRA có 2 nhà hàng trong khuôn viên, 3 quán bar, hồ bơi ngoài trời, trung tâm thể dục và quán bar." +
        //            " Khách sạn 5 sao này cũng có sảnh khách chung và dịch vụ concierge. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Peridot_Grand_Hotel_Spa_by_AIRA.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Solaria Hanoi Hotel",
        //            Address = "22 Bao Khanh, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc ở thành phố Hà Nội, cách Nhà Thờ Lớn 300 m, Solaria Hanoi Hotel có quầy bar, sân hiên và tầm nhìn ra quang cảnh thành phố." +
        //            " Trong số các tiện nghi của khách sạn này còn có nhà hàng, lễ tân 24 giờ, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên. Khách sạn cung cấp các phòng gia đình.",
        //            Image = "Solaria_Hanoi_Hotel.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Acoustic Hotel & Spa",
        //            Address = "39 Tho Nhuom, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, Acoustic Hotel & Spa có nhà hàng, xe đạp cho khách sử dụng miễn phí, trung tâm thể dục và quán bar." +
        //            " Khách sạn này có các phòng gia đình và sân hiên. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Acoustic_Hotel_Spa.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Sofitel Legend Metropole Hanoi",
        //            Address = "15 Ngo Quyen Street, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Là địa danh lịch sử sang trọng từ năm 1901, Sofitel Legend Metropole có các dịch vụ spa thư giãn, dịch vụ phòng 24 giờ và hồ bơi nước nóng." +
        //            " Khách sạn tọa lạc ở trung tâm thủ đô Hà Nội, gần Khu Phố Cổ.",
        //            Image = "Sofitel_Legend_Metropole_Hanoi.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Hanoi Paradise Center Hotel & Spa",
        //            Address = "22/5 Hang Voi Street, Ly Thai To Ward, Hoan Kiem District, Ha Noi, Viet Nam",
        //            Introduce = "Tọa lạc tại thành phố Hà Nội, cách Nhà hát múa rối nước Thăng Long 400 m, Hanoi Paradise Center Hotel & Spa cung cấp chỗ nghỉ với nhà hàng, chỗ đỗ xe riêng miễn phí, quán bar và sảnh khách chung." +
        //            " Khách sạn này có các phòng gia đình và sân hiên. Chỗ nghỉ cũng cung cấp dịch vụ lễ tân 24 giờ, dịch vụ phòng và dịch vụ thu đổi ngoại tệ cho khách.",
        //            Image = "Hanoi_Paradise_Center_Hotel_Spa.webp"
        //        });
        //    }

        //    // Ho Chi Minh.
        //    else if (locationName == "Hồ Chí Minh")
        //    {
        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Winsuites Saigon",
        //            Address = "28-30-32 Le Lai Street, Ben Thanh Ward, District 1, HCM City, Viet Nam",
        //            Introduce = "Tọa lạc tại Thành phố Hồ Chí Minh, Winsuites Saigon có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quán bar." +
        //            " Khách sạn 4 sao này còn có dịch vụ tiền sảnh và bàn đặt tour. Chỗ nghỉ cung cấp dịch vụ lễ tân 24 giờ, dịch vụ đưa đón sân bay, dịch vụ phòng và WiFi miễn phí trong toàn bộ khuôn viên.",
        //            Image = "Winsuites_Saigon.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Icon Saigon - LifeStyle Design Hotel",
        //            Address = "65-67 Hai Ba Trung, District 1, HCM City, Viet Nam",
        //            Introduce = "Tọa lạc ở Thành phố Hồ Chí Minh, Icon Saigon - LifeStyle Hotel có nhà hàng, hồ bơi ngoài trời, trung tâm thể dục và quầy bar." +
        //            " Khách sạn 4 sao này cung cấp WiFi miễn phí, dịch vụ lễ tân 24 giờ và dịch vụ phòng. Chỗ nghỉ nằm gần các điểm tham quan nổi tiếng như Trụ sở UBND Thành phố Hồ Chí Minh, Bưu điện Trung tâm Sài Gòn và Nhà thờ Đức Bà.",
        //            Image = "Icon_Saigon_LifeStyle_Design_Hotel.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "The Myst Dong Khoi",
        //            Address = "6-8 Ho Huan Nghiep, District 1, HCM City, Viet Nam",
        //            Introduce = "Với thiết kế và nội thất hiện đại cổ điển, The Myst Dong Khoi cung cấp chỗ nghỉ tinh tế ở khu trung tâm Quận 1 tại Thành phố Hồ Chí Minh." +
        //            " Du khách có thể ngâm mình thư giãn trong hồ bơi trên sân thượng, hoặc đơn giản là ngắm nhìn thành phố bên sông Sài Gòn từ sàn hồ bơi.",
        //            Image = "The_Myst_Dong_Khoi.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Grand Hotel Saigon",
        //            Address = "8 Dong Khoi Street, District 1, HCM City, Viet Nam",
        //            Introduce = "Nằm trong toà nhà kiểu thuộc địa được khôi phục lại, Grand Hotel Saigon cung cấp chỗ nghỉ rộng rãi tại Quận 1, thành phố Hồ Chí Minh." +
        //            " Khách sạn có hồ bơi ngoài trời, 2 nhà hàng trong khuôn viên và WiFi miễn phí.",
        //            Image = "Grand_Hotel_Saigon.jpg"
        //        });

        //        hotelsList.Add(new Hotel
        //        {
        //            HotelName = "Caravelle Saigon",
        //            Address = "19-23 Lam Son Square, District 1, HCM City, Viet Nam",
        //            Introduce = "Khai trương vào năm 1959, khách sạn 5 sao Caravelle Saigon có phong cách kiến trúc kiểu Pháp và Việt Nam đẹp mắt, hồ bơi ngoài trời cùng phòng không hút thuốc đi kèm Wi-Fi miễn phí." +
        //            " Nằm trong bán kính chỉ 50 m từ Nhà hát thành phố nổi tiếng ở Thành phố Hồ Chí Minh, khách sạn thân thiện với môi trường này cũng có quán bar trên sân thượng biểu diễn nhạc sống.",
        //            Image = "Caravelle_Saigon.jpg"
        //        });
        //    }

        //    LstHotel.ItemsSource = hotelsList;
        //}

        private async void InitilizeHotel(Location ltc)
        {
            HttpClient http = new HttpClient();
            
            var kq = await http.GetStringAsync
              ("http://tempwebapi.somee.com/api/ServiceController/GetHotelByLocation?ltID=" + ltc.LocationID.ToString());

            hotelList = JsonConvert.DeserializeObject<List<Hotel>>(kq);

            LstHotel.ItemsSource = hotelList;
        }

        private async void InitilizeHotelByLocationID(int ltcID)
        {
            HttpClient http = new HttpClient();

            var kq = await http.GetStringAsync
              ("http://tempwebapi.somee.com/api/ServiceController/GetHotelByLocation?ltID=" + ltcID.ToString());

            hotelList = JsonConvert.DeserializeObject<List<Hotel>>(kq);

            HttpClient http2 = new HttpClient();
            var kq2 = await http.GetStringAsync
              ("http://tempwebapi.somee.com/api/ServiceController/GetLocationNameByID?ltID=" + ltcID.ToString());

            var myDeserializedLocationName = JsonConvert.DeserializeObject<List<Location>>(kq2);

            Location locationNameJson = myDeserializedLocationName[0];

            Title = locationNameJson.LocationName;

            LstHotel.ItemsSource = hotelList;
        }

        private void LstHotel_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LstHotel.SelectedItem != null)
            {
                Hotel hotel = (Hotel)LstHotel.SelectedItem;

                Navigation.PushAsync(new HotelDetailsPage(hotel));
            }
            
        }
    }
}