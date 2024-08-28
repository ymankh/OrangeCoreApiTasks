using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
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
    }
}
