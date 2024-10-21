//HttpClient, Http isteği yapmaya ve onu yönetmeye yarar.
//HttpResponseMessage, Http yanıtlarını temsil eden bir nesnedir.İsteklerin sonucunda dönen durum kodu, başlıklar ve gövde gibi bilgileri içerir.
//EnsureSuccessStatusCode() metodu HTTP yanıtlarının başarıyla alınıp alınmadığını kontrol etmek için kullanılır.

using System.Text.Json;

using var httpClient = new HttpClient();
await GetJsonAsync(httpClient);
static async Task GetJsonAsync(HttpClient httpClient)
{
    HttpResponseMessage response = await httpClient.GetAsync("https://openlibrary.org/api/books?bibkeys=ISBN:0201558025,LCCN:93005405&format=json");
    var jsonResponse = await response.Content.ReadAsStringAsync();
    response.EnsureSuccessStatusCode()
        .WriteConsole();
    var data = JsonSerializer.Deserialize<dynamic>(jsonResponse);
    Console.WriteLine(data.Name);
    //Console.WriteLine($"{jsonResponse}\n");
}

static class HttpResponseMessageExtensions
{
    static public void WriteConsole(this HttpResponseMessage message)
    {
        if (message == null)
            return;

        var request = message.RequestMessage;
        Console.WriteLine(request?.Method);
        Console.WriteLine(request?.RequestUri);
        Console.WriteLine(request?.Version);
        
    }
}
