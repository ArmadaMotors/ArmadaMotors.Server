using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await _productService.RetrieveAllAsync());

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await _productService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(ProductForCreationDto dto)
            => Ok(await _productService.AddAsync(dto));
        
        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync(long id, ProductForCreationDto dto)
            => Ok(await _productService.ModifyAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(await _productService.RemoveAsync(id));

    }
}