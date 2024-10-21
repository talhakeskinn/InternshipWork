using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;


namespace PermissionAttributeProject
{

    public class Data
    {
        private readonly string _connectionString;

        public Data(string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PermissionV2Db;Integrated Security=True;Connect Timeout=30;Encrypt=False")
        {
            _connectionString = connectionString;
        }
       public bool ReturnPermissionResult(string CommandName, string UserName)
        {
            string query = "Select Count(*) from Permission where CommandId = @CommandId and UserId=@UserId";
            string userIdQuery = "Select UserId from Users where UserName = @UserName ";
            string commandIdQuery = "Select CommandId from Command where CommandName = @CommandName";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var UserCommand = new SqlCommand(userIdQuery, connection);
            UserCommand.Parameters.AddWithValue("UserName", UserName);
            using var CommandCommand = new SqlCommand(commandIdQuery, connection);
            CommandCommand.Parameters.AddWithValue("CommandName", CommandName);
            var CommandIdResult =(int) CommandCommand.ExecuteScalar();
            var UserIdResult =(int) UserCommand.ExecuteScalar();
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("CommandId", CommandIdResult);
            cmd.Parameters.AddWithValue("UserId", UserIdResult);
            var Result = (int)cmd.ExecuteScalar();
            connection.Close();

            return Result > 0;

        }
    }
}

