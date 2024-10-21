using Microsoft.Data.SqlClient;


namespace ReflectionProject.Datas.Connection
{
    public class ReflectionDbConnection
    {
        public SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_for_ReflectionProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
