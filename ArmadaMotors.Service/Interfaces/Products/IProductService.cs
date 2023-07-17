using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Products;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface IProductService
    {
        ValueTask<Product> RetrieveById(long id);
        ValueTask<IEnumerable<Product>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<Product> ModifyAsync(long id, ProductForUpdateDto dto);
        ValueTask<Product> AddAsync(ProductForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
        ValueTask<IEnumerable<Product>> SearchAsync(Filter filter);
        ValueTask<ProductPricesResultDto> RetrievePricesAsync(CurrencyType currencyType);
    }
}
