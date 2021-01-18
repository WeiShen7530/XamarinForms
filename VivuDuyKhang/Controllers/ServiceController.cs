using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using VivuDuyKhang.Database;
using System.Threading.Tasks;
using VivuDuyKhang.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using VivuDuyKhang.Providers;
using VivuDuyKhang.Results;
using System.Configuration;
using System.Data.SqlClient;

namespace VivuDuyKhang.Controllers
{
    public class ServiceController : ApiController
    {
        // SQLConnection
        static string SQLconnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(SQLconnectionString);

        // GetErrorResult
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        // GET api/ServiceController/Hello
        [Route("api/ServiceController/Hello")]
        [HttpGet]
        public IHttpActionResult HelloWord()
        {
            return Ok("Hello VivuDuyKhang API!");
        }

        // GET api/ServiceController/GetAllLocation
        [Route("api/ServiceController/GetAllLocation")]
        [HttpGet]
        public IHttpActionResult GetAllLocation()
        {
            try
            {
                DataTable kq = Database.Database.Read_Table("GetAllLocationProc");
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetLocationNameByID
        [Route("api/ServiceController/GetLocationNameByID")]
        [HttpGet]
        public IHttpActionResult GetLocationNameByID(int ltID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ltID", ltID);
                DataTable kq = Database.Database.Read_Table("GetLocationNameByIDProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetAllHotel
        [Route("api/ServiceController/GetAllHotel")]
        [HttpGet]
        public IHttpActionResult GetAllHotel()
        {
            try
            {
                DataTable kq = Database.Database.Read_Table("GetAllHotelProc");
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetHotelDetails
        [Route("api/ServiceController/GetHotelDetails")]
        [HttpGet]
        public IHttpActionResult GetHotelDetails(int htID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("htID", htID);
                DataTable kq = Database.Database.Read_Table("GetHotelDetailsProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetHotelByLocation
        [Route("api/ServiceController/GetHotelByLocation")]
        [HttpGet]
        public IHttpActionResult GetHotelByLocation(int ltID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ltID", ltID);
                DataTable kq = Database.Database.Read_Table("GetHotelByLocationProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetOrderInfo
        [Route("api/ServiceController/GetOrderInfo")]
        public List<OrderInfo> GetOrderInfo(int orderInfoID)
        {
            conn.Open();
            SqlDataAdapter sqlDA = new SqlDataAdapter();

            sqlDA.SelectCommand = new SqlCommand(
                "SELECT * FROM ORDERINFO WHERE OrderID=" + orderInfoID, conn);
            
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);
            conn.Close();

            var convertedList = (from rw in dt.AsEnumerable()
                                 select new OrderInfo()
                                 {
                                     OrderID = Convert.ToInt32(rw["OrderID"]),
                                     UserName = Convert.ToString(rw["UserName"]),
                                     UserEmail = Convert.ToString(rw["UserEmail"]),
                                     UserPhone = Convert.ToString(rw["UserPhone"]),
                                     UserAddress = Convert.ToString(rw["UserAddress"]),
                                     RoomPreference = Convert.ToString(rw["RoomPreference"]),
                                     NumberOfAdults = Convert.ToInt32(rw["NumberOfAdults"]),
                                     NumberOfChildren = Convert.ToInt32(rw["NumberOfChildren"]),
                                     SpecialRequests = Convert.ToString(rw["SpecialRequests"]),
                                     CheckInDate = Convert.ToDateTime(rw["CheckInDate"]),
                                     CheckOutDate = Convert.ToDateTime(rw["CheckOutDate"]),
                                     TotalMoney = Convert.ToDouble(rw["TotalMoney"])
                                 }).ToList();
            return convertedList;
        }

        // GET api/ServiceController/GetOrderInfoByID
        [Route("api/ServiceController/GetOrderInfoByID")]
        [HttpGet]
        public IHttpActionResult GetOrderInfoByID(int orderID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("orderID", orderID);
                DataTable kq = Database.Database.Read_Table("GetOrderInfoByIDProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetOrderInfoByOtherFields
        [Route("api/ServiceController/GetOrderInfoByOtherFields")]
        [HttpGet]
        public IHttpActionResult GetOrderInfoByOtherFields(string userName, string userEmail, string userPhone, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("userName", userName);
                param.Add("userEmail", userEmail);
                param.Add("userPhone", userPhone);
                param.Add("checkInDate", checkInDate);
                param.Add("checkOutDate", checkOutDate);
                DataTable kq = Database.Database.Read_Table("GetOrderInfoByOtherFieldsProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetOrderInfoByOthers
        [Route("api/ServiceController/GetOrderInfoByOthers")]
        [HttpGet]
        public IHttpActionResult GetOrderInfoByOthers(string userName, string userEmail, string userPhone)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("userName", userName);
                param.Add("userEmail", userEmail);
                param.Add("userPhone", userPhone);
                DataTable kq = Database.Database.Read_Table("GetOrderInfoByOthersProc", param);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetLastestOrder
        [Route("api/ServiceController/GetLastestOrder")]
        [HttpGet]
        public IHttpActionResult GetLastestOrder()
        {
            try
            {
                DataTable kq = Database.Database.Read_Table("GetLastestOrderInfoProc");
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/ServiceController/GetUsers
        [Route("api/ServiceController/GetUsers")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            try
            {
                DataTable kq = Database.Database.Read_Table("GetUserEmailProc");
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/ServiceController/InsertHotelRoomReservation
        [Route("api/ServiceController/InsertHotelRoomReservation")]
        public bool InsertHotelRoomReservation(OrderInfo orderInfo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO ORDERINFO (UserName,UserEmail,UserPhone,UserAddress,CheckInDate,CheckOutDate,RoomPreference,NumberOfAdults,NumberOfChildren,SpecialRequests,TotalMoney)" +
                "VALUES ('" + orderInfo.UserName + "','" + orderInfo.UserEmail + "','" + orderInfo.UserPhone + "','" + orderInfo.UserAddress + "','" + orderInfo.CheckInDate + "','" + orderInfo.CheckOutDate + "','" + orderInfo.RoomPreference + "','" + orderInfo.NumberOfAdults + "','" + orderInfo.NumberOfChildren + "','" + orderInfo.SpecialRequests + "','" + orderInfo.TotalMoney + "')";

            conn.Open();
            cmd.Connection = conn;

            int isSuccessQuery = cmd.ExecuteNonQuery();
            conn.Close();

            bool isSuccessTask = false;
            if (isSuccessQuery == 1)
            {
                isSuccessTask = true;
            }

            return isSuccessTask;
        }
    }
}
