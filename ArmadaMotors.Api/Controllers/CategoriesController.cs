using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace ArmadaMotors.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _categoryService.RetrieveAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _categoryService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(string name)
            => Ok(await _categoryService.AddAsync(name));

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync([FromRoute(Name = "Id")] long id, string name)
            => Ok(await _categoryService.ModifyAsync(id, name));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _categoryService.RemoveAsync(id));
    }
}