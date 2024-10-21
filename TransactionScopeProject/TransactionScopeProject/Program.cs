using System;
using System.Data.SqlClient;
using System.Transactions;


namespace TransactionScopeProject
{
  
    public static class Program
    {
        public static void Islem()
        {
            Product product = new Product();
            Category category = new Category();
            Console.Write("Product Name: ");
            product.Name = Console.ReadLine();
            Console.Write("Product Unit Price: ");
            product.UnitPrice = decimal.Parse(Console.ReadLine());
            Console.Write("Category Name: ");
            category.Name = Console.ReadLine();

            string CategoryConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CategoryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            string ProductConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            string GetCategoryId = "Insert Category values (@CategoryName); SELECT SCOPE_IDENTITY()";
            string InsertProduct = "INSERT INTO Product (ProductName, UnitPrice, CategoryId) VALUES (@ProductName, @UnitPrice, @CategoryId)";
            string getProduct = "Select * from Product";
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection categoryConnection = new SqlConnection(CategoryConnectionString))
                    {
                        categoryConnection.Open();
                        using (SqlCommand categoryCommand = new SqlCommand(GetCategoryId, categoryConnection))
                        {
                            categoryCommand.Parameters.AddWithValue("@CategoryName", category.Name);
                            var result = (categoryCommand.ExecuteScalar());
                            category.Id = Convert.ToInt32(result);
                        }
                    }
                    using (SqlConnection productConnection = new SqlConnection(ProductConnectionString))
                    {
                        productConnection.Open();
                        using (SqlCommand productCommand = new SqlCommand(InsertProduct, productConnection))
                        {
                            productCommand.Parameters.AddWithValue("@ProductName", product.Name);
                            productCommand.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                            productCommand.Parameters.AddWithValue("@CategoryId", category.Id);
                            productCommand.ExecuteNonQuery();
                        }
                    }
                    transactionScope.Complete();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata Oluştu " + ex.Message);
                }
            }
        }
        static void Main(string[] args)
        {
            Islem();
        }
    }

}
