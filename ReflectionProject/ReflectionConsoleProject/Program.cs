using Microsoft.Data.SqlClient;

#region Models
public class Person
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public byte Yas { get; set; }
}

public class Coordinate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Longitudine { get; set; }
    public decimal Latitudine { get; set; }
}

#endregion

#region DbConnection

public class DbConnection
{
    public SqlConnection connection = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_for_ReflectionProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    public SqlConnection GetConnection()
    {
        return new connection()
        {
            (
        };
    }
}
#endregion

#region GetDbTables 
public class GetDbTables
{
    string _query = String.Empty;

    DbConnection _dbConnection = new();

    SqlConnection con1 = _dbConnection.GetConnection();
#endregion