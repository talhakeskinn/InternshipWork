using System.Text.Json;

namespace StopajHesapAPI.Models
{
    public class ErrorDetails
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
