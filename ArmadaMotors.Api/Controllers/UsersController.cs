using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await _userService.RetrieveAllAsync());

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await _userService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(UserForCreationDto dto)
            => Ok(await _userService.AddAsync(dto));
        
        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync(long id, UserForCreationDto dto)
            => Ok(await _userService.ModifyAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(await _userService.RemoveAsync(id));
    }
}