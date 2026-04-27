using Microsoft.AspNetCore.Mvc;
using SF_API.Utils;

namespace SF_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var token = JwtGenerator.GenerateToken(_config);
            return Ok(token);
        }
    }
}
