using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Service.Interfaces.Products;

namespace ArmadaMotors.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IAssetService _assetService;

        public ProductService(IRepository<Product> productService,
            IRepository<Category> categoryService,
            IAssetService assetService)
        {
            _productRepository = productService;
            _categoryRepository = categoryService;
            _assetService = assetService;
        }

        public async ValueTask<Product> AddAsync(ProductForCreationDto dto)
        {
            var category = await this._categoryRepository.SelectByIdAsync(dto.CategoryId);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            // TODO: Initialize product details
            var product = new Product();
            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Assets = new List<ProductAsset>();

            // save assets
            foreach (var image in dto.Images)
            {
                var asset = await this._assetService.AddAsync(image);
                product.Assets.Add(new ProductAsset
                {
                    AssetId = asset.Id
                });
            }

            return await this._productRepository.InsertAsync(product);
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