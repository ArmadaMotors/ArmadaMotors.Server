using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ArmadaMotors.Service.Interfaces
{
    public interface IAssetService
    {
        ValueTask<Asset> RetrieveById(long id);
        ValueTask<IEnumerable<Asset>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<Asset> AddAsync(IFormFile file);
        ValueTask<bool> RemoveAsync(long id);
    }
}
