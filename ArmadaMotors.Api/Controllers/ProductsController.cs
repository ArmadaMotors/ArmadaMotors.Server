using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [ApiController, Authorize]
    [Route("Api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet, AllowAnonymous]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _productService.RetrieveAllAsync(@params));

        [HttpGet("{Id}"), AllowAnonymous]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _productService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromForm]ProductForCreationDto dto)
            => Ok(await _productService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync([FromRoute(Name = "Id")] long id, ProductForUpdateDto dto)
            => Ok(await _productService.ModifyAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _productService.RemoveAsync(id));

    }
}