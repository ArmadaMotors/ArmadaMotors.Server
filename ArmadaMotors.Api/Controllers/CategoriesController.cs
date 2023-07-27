using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet, AllowAnonymous]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _categoryService.RetrieveAllAsync(@params));

        [HttpGet("{Id}"), AllowAnonymous]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _categoryService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(CategoryForCreationDto dto)
            => Ok(await _categoryService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync([FromRoute(Name = "Id")] long id, CategoryForCreationDto dto)
            => Ok(await _categoryService.ModifyAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _categoryService.RemoveAsync(id));

    }
}