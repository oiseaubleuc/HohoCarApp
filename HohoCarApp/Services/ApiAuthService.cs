using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public class ApiAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecureStorage _secureStorage;

        public ApiAuthService(HttpClient httpClient, ISecureStorage secureStorage)
        {
            _httpClient = httpClient;
            _secureStorage = secureStorage;

            if (_httpClient.BaseAddress == null)
            {
                var baseUrl = "https://localhost:7231/";

                #if ANDROID
                baseUrl = "https://10.0.2.2:7231/";
                #endif

                _httpClient.BaseAddress = new Uri(baseUrl);
                
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "HohoCarApp-MAUI");
            }
        }

        public async Task<AuthResult> LoginAsync(string email, string password, bool rememberMe = false)
        {
            try
            {
                await Task.Delay(500); 
                
                var trimmedEmail = email?.Trim();
                var trimmedPassword = password?.Trim();
                

                
                if (trimmedEmail == "mooiweer@auto.be" && trimmedPassword == "Admin123!")
                {
                    var user = new User
                    {
                        Id = 1,
                        Email = "mooiweer@auto.be",
                        Username = "mooiweer",
                        FirstName = "Mooi",
                        LastName = "Weer",
                        PhoneNumber = "+32 123 456 789",
                        CreatedAt = DateTime.Now.AddDays(-30)
                    };

                    var token = "mock_token_" + Guid.NewGuid().ToString("N");
                    var refreshToken = "mock_refresh_" + Guid.NewGuid().ToString("N");

                    await _secureStorage.SetAsync("access_token", token);
                    await _secureStorage.SetAsync("refresh_token", refreshToken);
                    await _secureStorage.SetAsync("user_id", user.Id.ToString());


                    
                    return new AuthResult
                    {
                        IsSuccess = true,
                        Token = token,
                        RefreshToken = refreshToken,
                        User = user
                    };
                }
                else if (trimmedEmail == "admin@auto.be" && trimmedPassword == "Admin123!")
                {
                    var user = new User
                    {
                        Id = 2,
                        Email = "admin@auto.be",
                        Username = "admin",
                        FirstName = "Admin",
                        LastName = "User",
                        PhoneNumber = "+32 987 654 321",
                        CreatedAt = DateTime.Now.AddDays(-60)
                    };

                    var token = "mock_token_" + Guid.NewGuid().ToString("N");
                    var refreshToken = "mock_refresh_" + Guid.NewGuid().ToString("N");

                    await _secureStorage.SetAsync("access_token", token);
                    await _secureStorage.SetAsync("refresh_token", refreshToken);
                    await _secureStorage.SetAsync("user_id", user.Id.ToString());


                    
                    return new AuthResult
                    {
                        IsSuccess = true,
                        Token = token,
                        RefreshToken = refreshToken,
                        User = user
                    };
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Login failed - invalid credentials");
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid email or password. Please try again."
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                return new AuthResult
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred during login"
                };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/register", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var registerResponse = JsonSerializer.Deserialize<RegisterResponse>(responseContent);
                    
                    return new AuthResult
                    {
                        IsSuccess = true,
                        User = registerResponse.User
                    };
                }
                else
                {
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = errorResponse?.Message ?? "Registration failed"
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
                return new AuthResult
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred during registration"
                };
            }
        }

      


        public async Task<AuthResult> ForgotPasswordAsync(string email)
        {
            try
            {
                var request = new { Email = email };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/forgot-password", content);

                if (response.IsSuccessStatusCode)
                {
                    return new AuthResult { IsSuccess = true };
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = errorResponse?.Message ?? "Failed to send password reset email"
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Forgot password error: {ex.Message}");
                return new AuthResult
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while processing your request"
                };
            }
        }

        public async Task<bool> IsLoggedInAsync()
        {
            try
            {
                var token = await _secureStorage.GetAsync("access_token");
                return !string.IsNullOrEmpty(token) && token != "";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Check login status error: {ex.Message}");
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                // Call logout endpoint if needed
                await _httpClient.PostAsync("api/auth/logout", null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logout API error: {ex.Message}");
            }
            finally
            {
                // Clear stored tokens - use empty string instead of null
                await _secureStorage.SetAsync("access_token", "");
                await _secureStorage.SetAsync("refresh_token", "");
                await _secureStorage.SetAsync("user_id", "");
                
                System.Diagnostics.Debug.WriteLine("Logout successful - tokens cleared");
            }
        }

        public async Task<User> GetCurrentUserAsync()
        {
            try
            {
                var userId = await _secureStorage.GetAsync("user_id");
                if (string.IsNullOrEmpty(userId) || userId == "" || !int.TryParse(userId, out int id))
                    return null;

                // Return mock user based on ID
                return id switch
                {
                    1 => new User
                    {
                        Id = 1,
                        Email = "mooiweer@auto.be",
                        Username = "mooiweer",
                        FirstName = "Mooi",
                        LastName = "Weer",
                        PhoneNumber = "+32 123 456 789",
                        CreatedAt = DateTime.Now.AddDays(-30)
                    },
                    2 => new User
                    {
                        Id = 2,
                        Email = "admin@auto.be",
                        Username = "admin",
                        FirstName = "Admin",
                        LastName = "User",
                        PhoneNumber = "+32 987 654 321",
                        CreatedAt = DateTime.Now.AddDays(-60)
                    },
                    _ => null
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get current user error: {ex.Message}");
                return null;
            }
        }

        public async Task<AuthResult> RefreshTokenAsync()
        {
            try
            {
                var refreshToken = await _secureStorage.GetAsync("refresh_token");
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return new AuthResult { IsSuccess = false, ErrorMessage = "No refresh token available" };
                }

                var request = new { RefreshToken = refreshToken };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/refresh", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var refreshResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
                    
                    // Update stored tokens
                    await _secureStorage.SetAsync("access_token", refreshResponse.Token);
                    await _secureStorage.SetAsync("refresh_token", refreshResponse.RefreshToken);
                    
                    return new AuthResult
                    {
                        IsSuccess = true,
                        Token = refreshResponse.Token,
                        RefreshToken = refreshResponse.RefreshToken,
                        User = refreshResponse.User
                    };
                }
                else
                {
                    // Refresh token is invalid, clear stored tokens
                    await LogoutAsync();
                    return new AuthResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Session expired. Please login again."
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Refresh token error: {ex.Message}");
                return new AuthResult
                {
                    IsSuccess = false,
                    ErrorMessage = "An error occurred while refreshing the token"
                };
            }
        }
    }


}
