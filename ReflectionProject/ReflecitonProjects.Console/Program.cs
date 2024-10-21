using Microsoft.Data.SqlClient;
using ReflectionProject.Datas.Connection;

string query = "SELECT * FROM browser ";

ReflectionDbConnection nesne = new();

var connection = nesne.GetConnection();

SqlCommand command = new(query, connection);

try
{
    connection.Open();

    using (SqlDataReader reader = command.ExecuteReader())
    {
        // Verileri oku
        while (reader.Read())
        {
            reader.
            // Satırdaki verileri al
            Console.WriteLine($"{reader["Id"]}, {reader["Name"]}, {(DateTime)reader["EstablishmentDate"]},{reader["Company"]}");
        }
    }
}
catch (SqlException sqlEx)
{
    // SQL hatalarını yönet
    Console.WriteLine("SQL Hatası: " + sqlEx.Message);
    Console.WriteLine("Hata Kodu: " + sqlEx.ErrorCode);
    Console.WriteLine("Hata Kaynağı: " + sqlEx.Source);
}
catch (Exception ex)
{
    // Genel hataları yönet
    Console.WriteLine("Bir hata oluştu: " + ex.Message);
}