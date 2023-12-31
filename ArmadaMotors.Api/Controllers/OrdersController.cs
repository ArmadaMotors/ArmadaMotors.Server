using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost, AllowAnonymous]
        public async ValueTask<IActionResult> PostAsync(OrderForCreationDto dto)
            => Ok(await _orderService.AddAsync(dto));

        [HttpPatch("{Id}/Complete")]
        public async ValueTask<IActionResult> CompleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _orderService.CompleteAsync(id));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _orderService.RemoveAsync(id));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync(OrderStatus? status, [FromQuery] PaginationParams @params)
            => Ok(await _orderService.RetrieveAllAsync(status, @params));
    }
}