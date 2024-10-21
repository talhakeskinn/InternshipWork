using System.Text.Json;

Person person = new Person();
person.Name = "John";
person.Age = 20;

var jsonString = JsonSerializer.Serialize(person);
File.WriteAllText("deneme.json",jsonString);
public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
