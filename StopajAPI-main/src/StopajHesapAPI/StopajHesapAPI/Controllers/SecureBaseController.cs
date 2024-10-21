using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StopajHesapAPI.Models;

namespace StopajHesapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]

    
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class SecureBaseController : ControllerBase
    {
    }
}
