using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Users;

namespace ArmadaMotors.Service.Interfaces.Users
{
    public interface IFeedbackService
    {
        ValueTask<IEnumerable<Feedback>> RetrieveAllByProductIdAsync(long? productId, PaginationParams @params);
        ValueTask<IEnumerable<Feedback>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<Feedback> AddAsync(FeedbackForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
        ValueTask<Feedback> SetAvailabilityAsync(long id);
    }
}