using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password, bool rememberMe = false);
        Task<AuthResult> RegisterAsync(RegisterRequest request);

        Task<AuthResult> ForgotPasswordAsync(string email);
        Task<bool> IsLoggedInAsync();
        Task LogoutAsync();
        Task<User> GetCurrentUserAsync();
        Task<AuthResult> RefreshTokenAsync();
    }

    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; }
    }
}
