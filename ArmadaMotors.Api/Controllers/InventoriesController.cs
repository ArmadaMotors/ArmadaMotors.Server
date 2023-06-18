using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    //[Authorize]
    public class InventoriesController : BaseController
    {
        private readonly IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery]PaginationParams @params)
            => Ok(await this._inventoryService.RetrieveAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await this._inventoryService.RetrieveByIdAsync(id));

        [HttpGet("Product/ProductId")]
        public async ValueTask<IActionResult> GetByProductIdAsync([FromRoute(Name = "ProductId")] long productId)
            => Ok(await this._inventoryService.RetrieveByProductIdAsync(productId));

        [HttpPatch("Set")]
        public async ValueTask<IActionResult> SetAsync(long productId, int amount)
            => Ok(await this._inventoryService.SetAsync(productId, amount));
    }
}
