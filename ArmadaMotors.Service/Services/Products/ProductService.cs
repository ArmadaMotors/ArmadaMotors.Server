using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.EntityFrameworkCore;

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

        public async ValueTask<Product> ModifyAsync(long id, ProductForUpdateDto dto)
        {
            var product = await this._productRepository.SelectAll()
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                throw new ArmadaException(404, "Product not found");

            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;

            product.UpdatedAt = DateTime.UtcNow;

            await this._productRepository.SaveChangesAsync();

            return product;
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var product = await this._productRepository.SelectByIdAsync(id);
            if (product == null)
                throw new ArmadaException(404, "Product not found");

            return await this._productRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<Product>> RetrieveAllAsync(PaginationParams @params)
        {
            return await this._productRepository.SelectAll()
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .Include(p => p.Category)
                .ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<Product> RetrieveById(long id)
        {
            return await this._productRepository.SelectAll()
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async ValueTask<IEnumerable<Product>> SearchAsync(Filter filter)
        {
            var productsQuery = this._productRepository.SelectAll()
                .Include(p => p.Category)
                //.Include(p => p.Assets)
                .Where(p => p.Price >= filter.From && p.Price <= filter.To);
            
            if(filter.CategoryId != null)
                productsQuery = productsQuery.Where(p => p.CategoryId == filter.CategoryId);

            if (!string.IsNullOrEmpty(filter.Text))
            {
                productsQuery = productsQuery.Where(p =>
                p.Name.Contains(filter.Text, StringComparison.OrdinalIgnoreCase) &&
                p.Category.Name.Contains(filter.Text, StringComparison.OrdinalIgnoreCase));
            }

            return await productsQuery
                .ToPagedList(filter)
                .ToListAsync();
        }
    }
}