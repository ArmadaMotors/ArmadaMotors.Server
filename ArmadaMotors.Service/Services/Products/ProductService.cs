using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces;
using ArmadaMotors.Service.Interfaces.Products;
using ArmadaMotors.Shared.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        private readonly string _lang;

        public ProductService(IRepository<Product> productService,
            IRepository<Category> categoryService,
            IAssetService assetService,
            IMapper mapper)
        {
            _productRepository = productService;
            _categoryRepository = categoryService;
            _assetService = assetService;
            _mapper = mapper;

            _lang = HttpContextHelper.Language.ToLower();
        }

        public async ValueTask<Product> AddAsync(ProductForCreationDto dto)
        {
            var category = await this._categoryRepository.SelectByIdAsync(dto.CategoryId);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            // TODO: Initialize product details
            var product = this._mapper.Map<Product>(dto);
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

            this._mapper.Map(dto, product);
            product.UpdatedAt = DateTime.UtcNow;

            // init lang
            product.Name = _lang == "ru" 
                ? product.NameRu 
                : _lang == "en" 
                    ? product.NameEn 
                    : product.NameUz;

            product.Description = _lang == "ru" 
                ? product.DescriptionRu 
                : _lang == "en" 
                    ? product.DescriptionEn 
                    : product.DescriptionUz;

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
            var products = await this._productRepository.SelectAll()
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .Include(p => p.Category)
                .Include(p => p.Engines)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

            // init lang
            products.ForEach(p => 
            {
                p.Name = _lang == "ru" ? p.NameRu : _lang == "en" ? p.NameEn : p.NameUz;
                p.Description = _lang == "ru" ? p.DescriptionRu : _lang == "en" ? p.DescriptionEn : p.DescriptionUz;
            }); 

            return products;
        }

        public async ValueTask<Product> RetrieveById(long id)
        {
            var product = await this._productRepository.SelectAll()
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .Include(p => p.Category)
                .Include(p => p.Engines)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
                throw new ArmadaException(404, "Product not found");

            // init lang
            product.Name = _lang == "ru" 
                ? product.NameRu 
                : _lang == "en" 
                    ? product.NameEn 
                    : product.NameUz;

            product.Description = _lang == "ru" 
                ? product.DescriptionRu 
                : _lang == "en" 
                    ? product.DescriptionEn 
                    : product.DescriptionUz;

            return product;
        }

        public async ValueTask<ProductPricesResultDto> RetrievePricesAsync(CurrencyType currencyType)
        {
            var productsQuery = _productRepository.SelectAll()
                .Where(p => p.CurrencyType == currencyType)
                .OrderBy(p => p.Price);
            
            return new ProductPricesResultDto
            {
                From = (await productsQuery.FirstOrDefaultAsync())?.Price ?? 0,
                To = (await productsQuery.LastOrDefaultAsync())?.Price ?? 0
            };
        }

        public async ValueTask<int> RetrieveProductsCountAsync()
        {
            return await _productRepository.SelectAll().CountAsync();
        }

        public async ValueTask<IEnumerable<Product>> SearchAsync(Filter filter)
        {
            var productsQuery = this._productRepository.SelectAll()
                .Include(p => p.Category)
                .Include(p => p.Engines)
                .Include(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .AsQueryable();
            
            if(filter.From != null && filter.To != null)
            {
                productsQuery = productsQuery
                    .Where(p => p.Price >= filter.From && p.Price <= filter.To && p.CurrencyType == (filter.CurrencyType));
            }

            if (filter.CategoryIds != null)
                productsQuery = productsQuery.Where(p => filter.CategoryIds.Contains(p.CategoryId));

            if (!string.IsNullOrEmpty(filter.Text))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(filter.Text.ToLower()) ||
                    p.Category.Name.ToLower().Contains(filter.Text.ToLower()) ||
                    p.Description.ToLower().Contains(filter.Text.ToLower()) ||
                    p.NameUz.ToLower().Contains(filter.Text.ToLower()) || 
                    p.NameEn.ToLower().Contains(filter.Text.ToLower()) ||
                    p.NameRu.ToLower().Contains(filter.Text.ToLower()) ||
                    p.DescriptionUz.ToLower().Contains(filter.Text.ToLower()) ||
                    p.DescriptionEn.ToLower().Contains(filter.Text.ToLower()) ||
                    p.DescriptionRu.ToLower().Contains(filter.Text.ToLower()));
            }

            if (filter.CurrencyType!= null)
            {
                productsQuery = productsQuery.Where(p => 
                p.CurrencyType.Equals(filter.CurrencyType));
            }

            var products = await productsQuery
                .ToPagedList(filter)
                .ToListAsync();
            
            // init lang
            products.ForEach(p => 
            {
                p.Name = _lang == "ru" ? p.NameRu : _lang == "en" ? p.NameEn : p.NameUz;
                p.Description = _lang == "ru" ? p.DescriptionRu : _lang == "en" ? p.DescriptionEn : p.DescriptionUz;
            }); 

            return products;
        }
    }
}