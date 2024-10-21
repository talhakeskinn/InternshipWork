using CalculatorProject.Services.Abstract;

using Microsoft.AspNetCore.Mvc;

namespace CalculatorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        public readonly ICalculatorService _calculator;        
        public CalculatorController(ICalculatorService calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("Add")]
        IActionResult Add(RequestModel requestModel)
        {
            var result = _calculator.Add();
            return Ok(result);
        }
        [HttpPost("Substract")]
        IActionResult Substract(RequestModel requestModel)
        {
            var result = _calculator.Substract();
            return Ok(result);
        }
        [HttpPost("Multipy")]
        IActionResult Multipy(RequestModel requestModel)
        {
            var result = _calculator.Multiply();
            return Ok(result);
        }
        [HttpPost("Divide")]
        IActionResult Divide(RequestModel requestModel)
        {
            var result = _calculator.Divide();
            return Ok(result);
        }
    }
}
