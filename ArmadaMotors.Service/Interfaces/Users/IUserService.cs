using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Users;

namespace ArmadaMotors.Service.Interfaces.Users
{
    public interface IUserService
    {
        ValueTask<UserForResultDto> RetrieveByIdAsync(long id);
        ValueTask<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
        ValueTask<UserForResultDto> ModifyAsync(long id, UserForCreationDto dto);
        ValueTask<UserForResultDto> AddAsync(UserForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);

        ValueTask<UserForResultDto> RetrieveMeAsync();
    }
}