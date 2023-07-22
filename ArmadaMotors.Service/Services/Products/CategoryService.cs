using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async ValueTask<Category> AddAsync(CategoryForCreationDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            return await this._categoryRepository.InsertAsync(category);
        }

        public async ValueTask<Category> ModifyAsync(long id, CategoryForCreationDto dto)
        {
            var category = await this._categoryRepository.SelectByIdAsync(id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            category.NameUz = dto.NameUz;
            category.NameRu = dto.NameRu;
            category.NameEn = dto.NameEn;

            await this._categoryRepository.SaveChangesAsync();

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
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<Category> RetrieveById(long id)
        {
            var category = await this._categoryRepository.SelectAll()
                .Include(c => c.Products)
                .ThenInclude(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            return category;
        }
    }
}