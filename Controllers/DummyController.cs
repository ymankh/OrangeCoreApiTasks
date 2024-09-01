using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController(MyDbContext context) : ControllerBase
    {
        [HttpGet("{equation}")]
        public IActionResult Calculate(string equation)
        {
            if (equation.Contains('+'))
            {
                var equationParts = equation.Split('+');
                return Ok(Convert.ToDecimal(equationParts[0]) + Convert.ToDecimal(equationParts[1]));
            }

            if (equation.Contains('-'))
            {
                var equationParts = equation.Split('-');
                return Ok(Convert.ToDecimal(equationParts[0]) - Convert.ToDecimal(equationParts[1]));
            }

            if (equation.Contains('*'))
            {
                var equationParts = equation.Split('*');
                return Ok(Convert.ToDecimal(equationParts[0]) * Convert.ToDecimal(equationParts[1]));
            }

            if (equation.Contains('/'))
            {
                var equationParts = equation.Split('/');
                try
                {
                    return Ok(Convert.ToDecimal(equationParts[0]) / Convert.ToDecimal(equationParts[1]));
                }
                catch (DivideByZeroException e)
                {
                    return Ok(e.Message);
                }
            }

            return BadRequest();

        }

        [HttpGet("30problem")]
        public IActionResult Calculate(decimal first, decimal second)
        {
            if (first + second == 30 || first == 30 || second == 30)
                return Ok("The sum or one of the two numbers are 30");
            return Ok("neither of the two number nor the sum of them is 30.");

        }
        [HttpGet("dividableBy7or3")]
        public IActionResult DividableBy7Or3(decimal number)
        {
            return Ok(number > 0 && (number % 3 == 0 || number % 7 == 0));
        }

        [HttpGet("getProducts")]
        public IActionResult ProductsTask()
        {
            var orders = context.Products.OrderByDescending(p => p.ProductName).Take(5).Reverse();

            return Ok(orders.ToList());
        }
    }
}
