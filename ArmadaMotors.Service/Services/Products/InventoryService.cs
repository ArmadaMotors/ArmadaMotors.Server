using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.Services.Products
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> _inventoryRepository;
        private readonly IRepository<Product> _productRepository;

        public InventoryService(IRepository<Inventory> inventoryRepository, 
            IRepository<Product> productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }

        public async ValueTask<IEnumerable<Inventory>> RetrieveAllAsync(PaginationParams @params)
        {
            return await this._inventoryRepository.SelectAll()
                .Include(i => i.Product)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<Inventory> RetrieveByIdAsync(long id)
        {
            var inventory = await this._inventoryRepository.SelectAll()
                .Include(i => i.Product)
                .ThenInclude(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
            if (inventory == null)
                throw new ArmadaException(404, "Inventory not found");

            return inventory;
        }

        public async ValueTask<Inventory> RetrieveByProductIdAsync(long productId)
        {
            var inventory = await this._inventoryRepository.SelectAll()
                .Include(i => i.Product)
                .ThenInclude(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null)
                throw new ArmadaException(404, "Inventory not found");

            return inventory;
        }
        public async ValueTask<Inventory> SetAsync(long productId, int amount)
        {
            var product = await this._productRepository.SelectByIdAsync(productId);
            if (product == null)
                throw new ArmadaException(404, "Product not found");

            var inventory = await this._inventoryRepository.SelectAll()
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null)
            {
                return await this._inventoryRepository.InsertAsync(new Inventory
                {
                    ProductId = productId,
                    Amount = amount
                });
            };

            inventory.Amount = amount;
            await this._inventoryRepository.SaveChangesAsync();

            return inventory;
        }
    }
}
