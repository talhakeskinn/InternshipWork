namespace AdventureWorksAPIv3.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }

    public class PaginationResult<T>
    {
        public int PageCount { get; set; }
        public List<T> Result { get; set; }
    }
}

