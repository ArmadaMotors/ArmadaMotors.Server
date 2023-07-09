using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Products;

namespace ArmadaMotors.Service.Interfaces.Products
{
    public interface IOrderService
    {
        ValueTask<Order> AddAsync(OrderForCreationDto dto);
        ValueTask<Order> CompleteAsync(long id);
        ValueTask<IEnumerable<Order>> RetrieveAllAsync(OrderStatus? status, PaginationParams @params);
        ValueTask<bool> RemoveAsync(long id);
    }
}