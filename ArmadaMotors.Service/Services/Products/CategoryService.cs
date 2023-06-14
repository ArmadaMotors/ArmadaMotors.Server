using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Interfaces.Products;

namespace ArmadaMotors.Service.Services.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryService;

        public CategoryService(IRepository<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        public ValueTask<Category> AddAsync(string name)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Category> ModifyAsync(long id, string name)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<Category>> RetrieveAllAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<Category> RetrieveById(long id)
        {
            throw new NotImplementedException();
        }
    }
}