using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.RetrieveAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _userService.RetrieveByIdAsync(id));

        [HttpPost, AllowAnonymous]
        public async ValueTask<IActionResult> PostAsync([FromBody] UserForCreationDto dto)
            => Ok(await _userService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync([FromRoute(Name = "Id")] long id, [FromBody] UserForCreationDto dto)
            => Ok(await _userService.ModifyAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _userService.RemoveAsync(id));

        [HttpGet("Me"), Authorize]
        public async ValueTask<IActionResult> GetMeAsync()
            => Ok(await _userService.RetrieveMeAsync());

        [HttpPatch("{Id}/Role")]
        public async ValueTask<IActionResult> UpdateRoleAsync([FromRoute(Name = "Id")] long id, UserRole role)
            => Ok(await _userService.ModifyRoleAsync(id, role));

        [HttpGet("Test"), AllowAnonymous]
        public IActionResult GetTestResult()
            => Ok("Hello World!");
    }
}