using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Interfaces.Products;

namespace ArmadaMotors.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productService;

        public ProductService(IRepository<Product> productService)
        {
            _productService = productService;
        }

        public ValueTask<Product> AddAsync(ProductForCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Product> ModifyAsync(long id, ProductForCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<Product>> RetrieveAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public ValueTask<Product> RetrieveById(long id)
        {
            throw new NotImplementedException();
        }
    }
}