using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface IInventoryService
    {
        ValueTask<Inventory> SetAsync(long productId, int amount);
        ValueTask<Inventory> RetrieveByIdAsync(long id);
        ValueTask<Inventory> RetrieveByProductIdAsync(long productId);
        ValueTask<IEnumerable<Inventory>> RetrieveAllAsync(PaginationParams @params);
    }
}