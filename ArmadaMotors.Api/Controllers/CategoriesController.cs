using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await _categoryService.RetrieveAllAsync());

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync(long id)
            => Ok(await _categoryService.RetrieveById(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(string name)
            => Ok(await _categoryService.AddAsync(name));
        
        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> PutAsync(long id, string name)
            => Ok(await _categoryService.ModifyAsync(id, name));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(await _categoryService.RemoveAsync(id));
    }
}