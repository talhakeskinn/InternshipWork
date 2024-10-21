using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;
using TransactionProject.Services.Abstract;

namespace TransactionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IproductService _productService;
        public ProductController(IproductService productService)
        {
            _productService = productService;
        }

        [HttpPost("SaveProduct")]
        public IActionResult SaveProduct([FromBody] DtoProductCategory dtoProductCategory)
        {
            _productService.Save(dtoProductCategory);
            return Ok();
        }
    }
}
