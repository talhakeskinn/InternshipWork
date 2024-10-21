using System.ComponentModel.DataAnnotations;

namespace StopajHesapAPI.Entities
{
    public class RequestStopaj
    {
        [Required]
        public decimal matrah { get; set; }
        [Required]
        [Range(1,100)]
        public decimal oran { get; set; }
        [Required]
        public bool isNet { get; set; }
    }
}
