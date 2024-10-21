using System.ComponentModel.DataAnnotations;

namespace StopajHesapAPI.Models
{
    public class ResponseStopaj
    {
        public decimal Stopaj { get; set; }
        public decimal? newMatrah { get; set; }
    }
}
