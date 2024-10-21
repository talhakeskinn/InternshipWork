using AdventureWorksAPIv3.Entities;
using AdventureWorksAPIv3.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdventureWorksAPIv3.Repositories.Concrete
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString = "Server=**;Database=AdventureWorks2019;User Id = **; Password=**;TrustServerCertificate=true";

        int _pageCount = 0;
        public PaginationResult<Entities.Product> GetProductRepository(int PageNumber, int PageSize, string SchemaName, string TableName)
        {
            var products = new List<Entities.Product>();
            DataSet dataSet = new DataSet();
            using SqlConnection connection = new SqlConnection(_connectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            using var command = new SqlCommand("dbo.stpProductPagination", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@PageCount", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;
            command.Parameters.Add(outputParam);
            command.Parameters.AddWithValue("@PageNumber", PageNumber);
            command.Parameters.AddWithValue("@PageSize", PageSize);
            command.Parameters.AddWithValue("@SchemaName", SchemaName);
            command.Parameters.AddWithValue("@TableName", TableName);
            using SqlDataAdapter adapter = new(command);
            adapter.Fill(dataSet);
            if(dataSet.Tables.Count >0)
            {
                DataTable table = dataSet.Tables[0];
                var tableName = table.TableName;
                foreach (DataRow row in table.Rows)
                {
                    products.Add(new Entities.Product
                    {
                        ProductID = row.Field<int>($"{TableName}ID"),
                        Name = row.Field<string>("Name")
                    });
                }
                if(outputParam.Value != DBNull.Value)
                    _pageCount = (int)command.Parameters["@PageCount"].Value;
            }
            return new PaginationResult<Entities.Product>
            {
                Result = products,
                PageCount = _pageCount
            };
            //Bu classta generic bir type alıp onu döndürebiliriz.

            #region YanlışYol

            //connection.Open();
            //using var command = new SqlCommand("dbo.stpProductPagination ", connection);
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlParameter outputParam = new SqlParameter("@PageCount", SqlDbType.Int);
            //outputParam.Direction = ParameterDirection.Output;
            //command.Parameters.Add(outputParam);
            //command.Parameters.AddWithValue("@PageNumber", PageNumber);
            //command.Parameters.AddWithValue("@PageSize", PageSize);
            //command.Parameters.AddWithValue("@SchemaName", SchemaName);
            //command.Parameters.AddWithValue("@TableName", TableName);
            //using var reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    products.Add(new Product
            //    {
            //        ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
            //        Name = reader.GetString(reader.GetOrdinal("Name"))
            //    });
            //    _pageCount = Convert.ToInt32(command.Parameters["PageCount"].Value);
            //    //Dataset kullan, DataAdapter
            //}
            //return new PaginationResult<Product>
            //{
            //    Result = products,
            //    PageCount = _pageCount
            //};
            #endregion

        }
    }

}
