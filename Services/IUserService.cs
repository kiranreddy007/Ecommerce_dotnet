using EcommerceBackend.DTOs;

namespace EcommerceBackend.Services
{
    public interface IUserService
    {
        (bool Success, string Message) RegisterUser(RegisterRequest request);
        string LoginUser(LoginRequest request);
    }
}