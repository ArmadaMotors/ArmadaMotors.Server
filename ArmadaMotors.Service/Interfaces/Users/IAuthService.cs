using ArmadaMotors.Service.DTOs.Users;

namespace ArmadaMotors.Service.Interfaces.Users
{
    public interface IAuthService
    {
        ValueTask<LoginResultDto> AuthenticateAsync(LoginDto dto);
    }
}
