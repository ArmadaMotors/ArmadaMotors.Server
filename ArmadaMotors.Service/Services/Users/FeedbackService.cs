using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Users;
using ArmadaMotors.Shared.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Users
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IRepository<Feedback> feedbackRepository, 
            IRepository<Product> productRepository, 
            IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async ValueTask<Feedback> AddAsync(FeedbackForCreationDto dto)
        {
            var product = await _productRepository.SelectByIdAsync(dto.ProductId);
            if(product == null)
                throw new ArmadaException(404, "Product not found");

            var feedback = _mapper.Map<Feedback>(dto);
            feedback.UserId = HttpContextHelper.UserId ?? throw new UnauthorizedAccessException();

            return await _feedbackRepository.InsertAsync(feedback);
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var feedback = await _feedbackRepository.SelectByIdAsync(id);
            if(feedback == null)
                throw new ArmadaException(404, "Feedback not found");

            return await _feedbackRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<Feedback>> RetrieveAllAsync(PaginationParams @params)
        {
            return await _feedbackRepository.SelectAll()
                .Include(f => f.User)
                .Include(f => f.Product)
                .ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<IEnumerable<Feedback>> RetrieveAllByProductIdAsync(long? productId, PaginationParams @params)
        {
            var feedbackQuery = _feedbackRepository.SelectAll()
                .Include(f => f.User)
                .Include(f => f.Product)
                .Where(f => f.IsAvailable);

            if(productId != null)
                feedbackQuery = feedbackQuery.Where(f => f.ProductId == productId);

            return await feedbackQuery.ToPagedList(@params)
                .ToListAsync();
        }

        public async ValueTask<Feedback> SetAvailabilityAsync(long id)
        {
            var feedback = await _feedbackRepository.SelectByIdAsync(id);
            if(feedback == null)
                throw new ArmadaException(404, "Feedback not found");

            feedback.IsAvailable = true;
            
            await _feedbackRepository.SaveChangesAsync();

            return feedback;
        }
    }
}