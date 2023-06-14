using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Interfaces.Users;

namespace ArmadaMotors.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public ValueTask<User> AddAsync(UserForCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<User> ModifyAsync(long id, UserForCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<User>> RetrieveAllAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<User> RetrieveById(long id)
        {
            throw new NotImplementedException();
        }
    }
}