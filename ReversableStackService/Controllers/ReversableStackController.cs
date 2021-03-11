using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStackService.Models;

namespace ReversableStackService.Controllers
{
    [ApiController]
    [Route("[ReversableStackController]")]
    public class ReversableStackController : ControllerBase
    {
        private readonly IReversibleStack revStack;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public ReversableStackController()
        {
            revStack = new  ReversibleStack();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpPost]
        public ActionResult Push([FromBody]string input)
        {
            revStack.Push(input);
            return Ok();
        }

        [HttpGet]
        public ActionResult<string> Pop()
        {
            var res =  revStack.Pop();
            if (res == null)
                return NotFound();

            return Ok(res);
        }
        [HttpGet]
        public ActionResult<string> Peak()
        {
            var res = revStack.Peak();
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpPost]
        public ActionResult Revert()
        {
            revStack.Revert();
            return Ok();

        }
    }
}
