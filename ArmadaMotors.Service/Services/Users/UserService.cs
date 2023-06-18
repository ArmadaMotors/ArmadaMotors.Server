using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Configurations;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Domain.Enums;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Extensions;
using ArmadaMotors.Service.Interfaces.Users;
using ArmadaMotors.Shared.Helpers;
using AutoMapper;
using FleetFlow.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async ValueTask<UserForResultDto> AddAsync(UserForCreationDto dto)
        {
            var user = this._mapper.Map<User>(dto);

            var existUser = await this._userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existUser != null)
                throw new ArmadaException(400, "User already exist");

            user.Password = PasswordHelper.Hash(user.Password);
            var result = await this._userRepository.InsertAsync(user);

            return this._mapper.Map<UserForResultDto>(result);
        }

        public async ValueTask<UserForResultDto> ModifyAsync(long id, UserForCreationDto dto)
        {
            var oldUser = await this._userRepository.SelectByIdAsync(id);
            if (oldUser == null)
                throw new ArmadaException(404, "User not found");

            var existUser = await this._userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (existUser != null && oldUser.Username != dto.Username)
                throw new ArmadaException(400, "Username is already taken");

            var user = this._mapper.Map(dto, existUser);

            user.Id = id;
            user.Password = PasswordHelper.Hash(user.Password);
            var result = await this._userRepository.UpdateAsync(user);

            return this._mapper.Map<UserForResultDto>(result);
        }

        public async ValueTask<UserForResultDto> ModifyRoleAsync(long id, UserRole role)
        {
            var user = await this._userRepository.SelectByIdAsync(id);
            if (user == null)
                throw new ArmadaException(404, "User not found");

            user.Role = role;

            await this._userRepository.SaveChangesAsync();

            return this._mapper.Map<UserForResultDto>(user);
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var existUser = await this._userRepository.SelectByIdAsync(id);
            if (existUser == null)
                throw new ArmadaException(404, "User not found");

            return await this._userRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var users = await this._userRepository.SelectAll()
                .ToPagedList(@params)
                .ToListAsync();

            return this._mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async ValueTask<UserForResultDto> RetrieveByIdAsync(long id)
        {
            var user = await this._userRepository.SelectByIdAsync(id);
            if (user == null)
                throw new ArmadaException(404, "User not found");

            return this._mapper.Map<UserForResultDto>(user);
        }

        public async ValueTask<UserForResultDto> RetrieveMeAsync()
        {
            var user = await this._userRepository.SelectByIdAsync(
                HttpContextHelper.UserId ?? throw new ArmadaException(401, "Unauthorized action"));

            return this._mapper.Map<UserForResultDto>(user);
        }
    }
}