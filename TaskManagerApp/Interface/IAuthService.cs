using TaskManagerApp.DTOs.AuthDTOs;

namespace TaskManagerApp.Interface
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto dto);
        Task<AuthDto> LoginAsync(LoginDto dto);
        Task<string> AddRoleAsync(AddRoleDto dto);

    }
}
