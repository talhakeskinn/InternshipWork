using System.Data.SqlTypes;

namespace TransactionProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

    }
    class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

    }
}
