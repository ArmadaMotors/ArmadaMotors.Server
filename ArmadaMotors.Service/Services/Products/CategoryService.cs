using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using ArmadaMotors.Shared.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ArmadaMotors.Service.Services.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly string _lang;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

            _lang = HttpContextHelper.Language.ToLower();
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

            var areExistChilds = this._categoryRepository.SelectAll()
                .Any(cc => cc.ParentId == id);
            if(areExistChilds)
                throw new ArmadaException(400, "Please, there are related child categories, please, remove them before");
                
            return await this._categoryRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<Category>> RetrieveAllAsync(PaginationParams @params)
        {
            var categories = await this._categoryRepository.SelectAll()
                .Include(c => c.Products)
                .Include(c => c.Parent)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

            // init lang
            categories.ForEach(c => c.Name = _lang == "ru" ? c.NameRu : _lang == "en" ? c.NameEn : c.NameUz);

            return categories;
        }

        public async ValueTask<Category> RetrieveById(long id)
        {
            var category = await this._categoryRepository.SelectAll()
                .Include(c => c.Parent)
                .Include(c => c.Products)
                .ThenInclude(p => p.Assets)
                .ThenInclude(a => a.Asset)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                throw new ArmadaException(404, "Category not found");

            // init lang
            category.Name = _lang == "ru" ? category.NameRu : _lang == "en" ? category.NameEn : category.NameUz;

            return category;
        }

		public async ValueTask<IEnumerable<Category>> RetrieveByIds(IEnumerable<long> ids)
		{
			// Use LINQ to filter categories based on the given IDs
			var categories = await this._categoryRepository.SelectAll()
		        .Include(c => c.Products)
		        .Include(c => c.Parent)
		        .AsNoTracking()
		        .Where(c => ids.Contains(c.Id))
		        .ToListAsync();

            if (categories.Count==0)
                throw new ArmadaException(404, "Categories not found");
			// Initialize the language for each category
			categories.ForEach(c => c.Name = _lang == "ru" ? c.NameRu : _lang == "en" ? c.NameEn : c.NameUz);

			return categories;
		}
	}
}