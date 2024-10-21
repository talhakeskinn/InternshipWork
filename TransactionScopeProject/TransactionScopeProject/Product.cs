using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionScopeProject
{
    public class Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Product : Base
    {
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
    public class Category : Base
    {

    }
}
