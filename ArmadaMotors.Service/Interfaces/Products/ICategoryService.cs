using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface ICategoryService
    {
        ValueTask<Category> RetrieveById(long id);
        ValueTask<IEnumerable<Category>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<Category> ModifyAsync(long id, string name);
        ValueTask<Category> AddAsync(string name);
        ValueTask<bool> RemoveAsync(long id);
    }
}
