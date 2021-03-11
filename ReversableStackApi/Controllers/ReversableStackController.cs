using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReversableStackApi.Models;


namespace MyStackService.Controllers
{
    public class ReversibleStackController : ApiController
    {
        private static readonly IReversibleStack<string> revStack;

        static ReversibleStackController()
        {
            revStack = new ReversibleStack<string>();
        }

        [Route("api/ReversibleStack/Push")]
        [HttpPost]
        public HttpResponseMessage Push([FromUri] string input)
        {
            if(input==null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "input cannot be null");

            revStack.Push(input);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/ReversibleStack/Pop")]
        [HttpGet]
        public HttpResponseMessage Pop()
        {
            return VerifyOutput(revStack.Pop());
        }


        [Route("api/ReversibleStack/Peak")]
        [HttpGet]
        public HttpResponseMessage Peak()
        {
            return VerifyOutput(revStack.Peak());
        }
        [Route("api/ReversibleStack/Revert")]
        [HttpGet]
        public void Revert()
        {
            revStack.Revert();
        }
        private HttpResponseMessage VerifyOutput(string output)
        {
            if (EqualityComparer<string>.Default.Equals(output, default(string)))
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Stack is empty");

            }
            return Request.CreateResponse<string>(HttpStatusCode.OK, output);
        }
    }
}