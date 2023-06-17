using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ArmadaMotors.Service.Services.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async ValueTask<Category> AddAsync(string name)
        {
            return await this._categoryRepository.InsertAsync(new Category
            { 
                Name = name 
            });
        }

        public async ValueTask<Category> ModifyAsync(long id, string name)
        {
            var category = await this._categoryRepository.SelectByIdAsync(id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            category.Name = name;

            return category;
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var category = await this._categoryRepository.SelectByIdAsync(id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            return await this._categoryRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<Category>> RetrieveAllAsync(PaginationParams @params)
        {
            return await this._categoryRepository.SelectAll()
                .Include(c => c.Products)
                .ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<Category> RetrieveById(long id)
        {
            var category = await this._categoryRepository.SelectAll()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            return category;
        }
    }
}