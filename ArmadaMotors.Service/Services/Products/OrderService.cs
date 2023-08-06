using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Products;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Products
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, 
            IRepository<Product> productRepository, 
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async ValueTask<Order> AddAsync(OrderForCreationDto dto)
        {
            var existProduct = await _productRepository.SelectByIdAsync(dto.ProductId);
            if(existProduct == null)
                throw new ArmadaException(404, "Product not found");

            var order = _mapper.Map<Order>(dto);

            return await _orderRepository.InsertAsync(order);
        }

        public async ValueTask<Order> CompleteAsync(long id)
        {
            var order = await _orderRepository.SelectByIdAsync(id);
            if(order == null)
                throw new ArmadaException(404, "Order not found");
            
            order.Status = OrderStatus.Completed;

            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var order = await _orderRepository.SelectByIdAsync(id);
            if (order == null)
                throw new ArmadaException(404, "Order not found");

            return await _orderRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<Order>> RetrieveAllAsync(OrderStatus? status, PaginationParams @params)
        {
            var ordersQuery = _orderRepository.SelectAll();

            if(status != null)
                ordersQuery = ordersQuery.Where(o => o.Status == status);
            
            return await ordersQuery.ToPagedList(@params)
                .ToListAsync();
        }
    }
}