using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1asdf.Models;

namespace WebApplication1asdf.Controllers
{
    public class DefaultController : ApiController
    {
        [System.Web.Http.Route("api/Default/Get")]
        [System.Web.Http.HttpGet]
        public string Get()
        {
            return Default.bla;
        }
    }
}
