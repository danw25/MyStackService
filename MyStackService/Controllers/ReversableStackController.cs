using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MyStackService.Models;

namespace MyStackService.Controllers
{
    public class ReversibleStackController : ApiController
    {
        private readonly IReversibleStack revStack;

        public ReversibleStackController()
        {
            revStack = new ReversibleStack();
        }

        [System.Web.Http.Route("api/ReversibleStack/Push")]
        [System.Web.Http.HttpPost]
        public void Push([FromBody] string input)
        {

            revStack.Push(input);
        }

        [System.Web.Http.Route("api/ReversibleStack/Pop")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Pop()
        {
            return VerifyOutput(revStack.Pop());
        }


        [System.Web.Http.Route("api/ReversibleStack/Peak")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Peak()
        {
            return VerifyOutput(revStack.Peak());
        }
        [System.Web.Http.Route("api/ReversibleStack/Revert")]
        [System.Web.Http.HttpGet]
        public void Revert()
        {
            revStack.Revert();
        }
        private HttpResponseMessage VerifyOutput(string output)
        {
            if (EqualityComparer<string>.Default.Equals(output, default(string)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stack is empty");

            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, output);
        }
    }
}
