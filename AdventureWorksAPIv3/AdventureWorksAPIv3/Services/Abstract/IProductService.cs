using AdventureWorksAPIv3.Entities;

namespace AdventureWorksAPIv3.Repositories.Abstract
{
    public interface IProductService
    {
        public PaginationResult<Product> GetProductRepository(int PageNumber, int PageSize, string SchemaName, string TableName);
            
    }
}
