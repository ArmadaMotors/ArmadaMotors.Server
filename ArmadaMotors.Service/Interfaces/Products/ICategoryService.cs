using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface ICategoryService
    {
        ValueTask<Category> RetrieveById(long id);
        ValueTask<IEnumerable<Category>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<Category> ModifyAsync(long id, CategoryForCreationDto dto);
        ValueTask<Category> AddAsync(CategoryForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
    }
}
