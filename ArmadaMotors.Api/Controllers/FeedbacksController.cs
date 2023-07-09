using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Service.Interfaces.Users;
using ArmadaMotors.Domain.Configurations;
using Microsoft.AspNetCore.Mvc;
using ArmadaMotors.Service.DTOs.Users;

namespace ArmadaMotors.Api.Controllers
{
    public class FeedbacksController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync(long productId, PaginationParams @params)
            => Ok(await _feedbackService.RetrieveAllAsync(productId, @params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _feedbackService.RemoveAsync(id));

        [HttpPatch("{Id}")]
        public async ValueTask<IActionResult> SetAvailabilityAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _feedbackService.SetAvailabilityAsync(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(FeedbackForCreationDto dto)
            => Ok(await _feedbackService.AddAsync(dto));
    }
}