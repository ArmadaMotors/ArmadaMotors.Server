using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Users;

namespace ArmadaMotors.Service.Interfaces.Users
{
    public interface IUserService
    {
        ValueTask<User> RetrieveById(long id);
        ValueTask<IEnumerable<User>> RetrieveAllAsync();
        ValueTask<User> ModifyAsync(long id, UserForCreationDto dto);
        ValueTask<User> AddAsync(UserForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
    }
}