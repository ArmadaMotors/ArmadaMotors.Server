using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface ICategoryService
    {
        ValueTask<Category> RetrieveById(long id);
        ValueTask<IEnumerable<Category>> RetrieveAllAsync();
        ValueTask<Category> ModifyAsync(long id, string name);
        ValueTask<Category> AddAsync(string name);
        ValueTask<bool> RemoveAsync(long id);
    }
}
