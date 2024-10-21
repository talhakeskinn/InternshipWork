using AdventureWorksAPIv3.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksAPIv3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _repository;
        public ProductController(IProductService repository)
        {
            _repository = repository;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int PageNumber, int PageSize, string SchemaName, string TableName)
        {
            var result = _repository.GetProductRepository(PageNumber, PageSize, SchemaName, TableName);
            return Ok(result);
        }
    }
}
