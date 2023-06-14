using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface IProductService
    {
        ValueTask<Product> RetrieveById(long id);
        ValueTask<IEnumerable<Product>> RetrieveAllAsync();
        ValueTask<Product> ModifyAsync(long id, ProductForCreationDto dto);
        ValueTask<Product> AddAsync(ProductForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
    }
}
