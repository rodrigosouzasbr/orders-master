using Microsoft.AspNetCore.Mvc;
using Sales.Orders.Application.Services;

namespace Sales.Orders.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
    
            var token = _jwtService.GenerateToken("1", "rodrigo@email.com");

            return Ok(new { token });
        }
    }
}
