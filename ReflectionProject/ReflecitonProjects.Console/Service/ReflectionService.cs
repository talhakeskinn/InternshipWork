using Microsoft.Data.SqlClient;
using ReflectionProject.Datas.Connection;

namespace ReflecitonProjects.Console.Service
{
    public class ReflectionService
    {
        private ReflectionDbConnection _dbConnectionClass;
        private SqlConnection _connection;
        public ReflectionService(ReflectionDbConnection dbConnectionClass, SqlConnection connection)
        {
            _dbConnectionClass = dbConnectionClass;
            _connection = connection;
        
    }
}
