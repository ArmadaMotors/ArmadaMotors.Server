using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Service.Interfaces.Users;
using ArmadaMotors.Domain.Configurations;
using Microsoft.AspNetCore.Mvc;
using ArmadaMotors.Service.DTOs.Users;
using Microsoft.AspNetCore.Authorization;

namespace ArmadaMotors.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FeedbacksController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("Availables"), AllowAnonymous]
        public async ValueTask<IActionResult> GetAllByProductIdAsync(long? productId, [FromQuery] PaginationParams @params)
            => Ok(await _feedbackService.RetrieveAllByProductIdAsync(productId, @params));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _feedbackService.RetrieveAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _feedbackService.RemoveAsync(id));

        [HttpPatch("{Id}/Availability")]
        public async ValueTask<IActionResult> SetAvailabilityAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _feedbackService.SetAvailabilityAsync(id));

        [HttpPost, AllowAnonymous]
        public async ValueTask<IActionResult> PostAsync(FeedbackForCreationDto dto)
            => Ok(await _feedbackService.AddAsync(dto));
    }
}