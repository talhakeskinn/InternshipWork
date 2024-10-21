//using Microsoft.Data.SqlClient;

//string TableName = "Person";
//string query = "Select * from Person";
//using (SqlConnection connection = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_for_ReflectionProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
//{
//    SqlCommand command = new(query, connection);
//    connection.Open();

//    List<Person> persons = new List<Person>();

//    using (var reader = command.ExecuteReader())
//    {
//        var type = typeof(Person);
//        while (reader.Read())
//        {
//            Person person = new Person();
//            object[] values = new object[reader.FieldCount];
//            reader.GetValues(values);



//            for (int i = 0; i < values.Length; i++)
//            {
//                string columnName = reader.GetName(i);
//                var property = type.GetProperty(columnName);
//                if (property == null)
//                    continue;
//                property.SetValue(person, values[i]);
//                Console.WriteLine($"Column: {columnName} Values: {values[i]} Type:{reader.GetDataTypeName(i)}" );
//            } 
//            persons.Add(person);
//        } 
//    }
//    for(int i = 0; i < persons.Count;i++)
//    {
//        Console.WriteLine(persons[i].Id.ToString()," ", persons[i].Ad.ToString());
//    }
//}


//#region Models
//public class Person
//{
//    public int Id { get; set; }
//    public string Ad { get; set; }
//    public string Soyad { get; set; }
//    public byte Yas { get; set; }
//}
//public class Coordinate
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public decimal Longitudine { get; set; }
//    public decimal Latitudine { get; set; }
//}
//#endregion



using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;



var result = Query<Person>(sql: "Select * from Person where Ad = @ad and Soyad = @soyad",
    parameter: new
    {
        ad = "Bill",
        soyad = "Gates"
    });
for (int i = 0; i < result.Count; i++)
{
    Console.WriteLine(result[i].Id.ToString() + " " + result[i].Ad.ToString() + " " + result[i].Soyad.ToString() + " " + result[i].Yas.ToString());
}
Console.ReadKey();

List<T> Query<T>(string sql, dynamic? parameter) where T : class
{
    List<T> entityList = new();
    using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_for_ReflectionProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    connection.Open();
    using var cmd = new SqlCommand(sql, connection);

    #region AddDynamicTypeValuePart1
    //if (parameter != null)
    //{
    //    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(parameter))
    //    {
    //        cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(parameter));
    //    }
    //}
    #endregion

    #region AddDynamicTypeValuePart2
    if (parameter != null)
    {
        var parameterToObject = (object)parameter;
        PropertyInfo[] propInfo = parameterToObject.GetType().GetProperties();
        foreach (var prop in propInfo)
        {
            cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(parameterToObject));
        }
    }
    //PropertyInfo sınıfı, bir özelliğin adını, türünü, erişim belirleyicilerini (public, private vb.), ve özellik üzerinde yapılan diğer işlemleri (örneğin okuma veya yazma) temsil eder. Özellikle yansıma (reflection) kullanılarak çalışma zamanında türlerin, özelliklerin ve diğer üyelerin bilgilerini dinamik olarak elde etmek için kullanılır.
    #endregion

    using var reader = cmd.ExecuteReader();

    var type = typeof(T);
    while (reader.Read())
    {
        var obj = Activator.CreateInstance(type);
        object[] values = new object[reader.FieldCount];
        reader.GetValues(values);

        for (int i = 0; i < values.Length; i++)
        {
            string columName = reader.GetName(i);
            var property = type.GetProperty(columName);
            if (property == null)
                continue;
            property.SetValue(obj, values[i]);
        }
        entityList.Add((T)obj);
    }


    return entityList;
}

public class Person
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public int Yas { get; set; }
}
