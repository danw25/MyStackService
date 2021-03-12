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
            try
            {
             if(input==null)
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "input cannot be null");

                        revStack.Push(input);
                        return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return ReportError("Push");
            }

           
        }

        [Route("api/ReversibleStack/Pop")]
        [HttpGet]
        public HttpResponseMessage Pop()
        {
            try
            {
                return VerifyOutput(revStack.Pop());
            }
            catch (Exception)
            {

               return ReportError("Pop");
            }
            
        }


        [Route("api/ReversibleStack/Peak")]
        [HttpGet]
        public HttpResponseMessage Peak()
        {
            try
            {
                return VerifyOutput(revStack.Peak());
            }
            catch (Exception)
            {

                return ReportError("Peak");
            }
        }
        [Route("api/ReversibleStack/Revert")]
        [HttpGet]
        public void Revert()
        {
            try
            {
                revStack.Revert();
            }
            catch (Exception)
            {
                ReportError("Revert");
            }
            
        }
        private HttpResponseMessage VerifyOutput(string output)
        {
            try
            {
                if (EqualityComparer<string>.Default.Equals(output, default(string)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Stack is empty");

                }
                return Request.CreateResponse<string>(HttpStatusCode.OK, output);
            }
            catch (Exception)
            {
                return ReportError("VerifyOutput");
            }
            
        }

        private HttpResponseMessage ReportError(string method)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error from {method}");
        }
    }
}
