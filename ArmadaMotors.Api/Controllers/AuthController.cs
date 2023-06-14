using ArmadaMotors.Service.DTOs;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async ValueTask<IActionResult> LoginAsync([FromBody]LoginDto dto)
            => Ok(await _authService.AuthenticateAsync(dto));

    }
}
