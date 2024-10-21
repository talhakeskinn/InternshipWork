using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StopajHesapAPI.Entities;
using StopajHesapAPI.Models;

namespace StopajHesapAPI.Controllers
{

    public class KdvContreller : SecureBaseController
    {
        [HttpPost("KdvHesapla")]
        public IActionResult KdvHesapla(RequestStopaj entity)
        {

            return Ok(null);

        }
    }
}
