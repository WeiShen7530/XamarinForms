using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VivuDuyKhang.Controllers
{
    public class DraftController : ApiController
    {
        [Route("api/DraftController/Hello")]
        [HttpGet]

        public IHttpActionResult Hello()
        {
            return Ok("Hello VivuDuyKhang API!");
        }
    }
}
