using Microsoft.Data.SqlClient;
using TransactionProject.Models;
using TransactionProject.Services.Abstract;

namespace TransactionProject.Services.Concrete
{
    public class ProductService : IproductService
    {
        public void Save(DtoProductCategory dtoProductCategory)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=transection_project;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            string cmdInsertCategory = "Insert Category values (@CategoryName); SELECt SCOPE_IDENTITY()";
            string cmdInsertProduct = "INSERT INTO Product (ProductName, UnitPrice, CategoryId) VALUES (@ProductName, @UnitPrice, @CategoryId)";


            SqlConnection? connection = null;

            SqlTransaction? transaction = null;

            try
            {
                connection = new SqlConnection(connectionString);

                connection.Open();

                transaction = connection.BeginTransaction();
                using var command = new SqlCommand(cmdInsertCategory, connection, transaction);
                command.Parameters.AddWithValue("@CategoryName", dtoProductCategory.CategoryName);
                object result = command.ExecuteScalar();
                int a = 0;
                decimal x = 20 / a;


                if (result == null)
                {
                    transaction.Rollback();
                    throw new Exception("Girdiğiniz kategori mevcut değildir");
                }

                decimal categoryId = (decimal)result;

                using var insertCommand = new SqlCommand(cmdInsertProduct, connection, transaction);
                insertCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                insertCommand.Parameters.AddWithValue("@ProductName", dtoProductCategory.ProductName);
                insertCommand.Parameters.AddWithValue("@UnitPrice", dtoProductCategory.Unitprice);
                insertCommand.ExecuteNonQuery();

                transaction?.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                transaction?.Dispose();
                connection?.Dispose();
            }
        }
    }
}
