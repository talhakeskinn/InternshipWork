using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StopajHesapAPI.Entities;
using StopajHesapAPI.Middlewares;
using StopajHesapAPI.Models;
using StopajHesapAPI.Services.Abstraction;
using StopajHesapAPI.Services.Concerete;

namespace StopajHesapAPI.Controllers
{
   
    public class StopajHesapController : SecureBaseController
    {
        readonly IStopajHesapService _service;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;


        public StopajHesapController(IStopajHesapService service, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _service = service;
            _logger = logger;
        }
        [ProducesResponseType(typeof(ResponseStopaj), StatusCodes.Status200OK)]
        [HttpPost("GetStopaj")]
        public IActionResult GetStopaj(RequestStopaj entity)
        {
            ResponseStopaj sonuc = _service.Hesapla(entity);
            return Ok(sonuc);
        }
    }
}

