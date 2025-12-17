using Blazored.LocalStorage;
using metiers.shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace front.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserServices (HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Sauvegarde du token et infos dans le localStorage
        private async Task SaveUserDataAsync(LoginResult result)
        {
            await _localStorage.SetItemAsync("token", result.token);
            await _localStorage.SetItemAsync("email", result.email);
            await _localStorage.SetItemAsync("userId", result.id);
            await _localStorage.SetItemAsync("role", result.role);
            await AddJwtHeaderAsync();
        }

        // Ajoute le JWT dans l'en-tête Authorization
        public async Task AddJwtHeaderAsync( )
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> Register(UserDTO newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/Register", newUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null) return false;

            var response = await _httpClient.PostAsJsonAsync("api/Account/Login", loginDTO);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result == null) return false;

            await SaveUserDataAsync(result);
            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("email");
            await _localStorage.RemoveItemAsync("userId");
            await _localStorage.RemoveItemAsync("role");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserDTO> GetCurrentUser()
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.GetAsync("api/Account/Me");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

        public async Task<string> GetRoleAsync()
        {
            await AddJwtHeaderAsync();
            return await _localStorage.GetItemAsync<string>("role");
        }
        public async Task<List<UserDTO>> GetPharmacies()
        {
            await AddJwtHeaderAsync();

            return await _httpClient.GetFromJsonAsync<List<UserDTO>>(
                "api/User/pharmaciens"
            ) ?? new List<UserDTO>();
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Account", user);

            return response.IsSuccessStatusCode;
        }
        public async Task<UserDTO> GetDoctor(Guid medecinId)
        {
            return await _httpClient.GetFromJsonAsync<UserDTO>(
                $"api/Account/Doctor/{medecinId}"
            );
        }
    }
}
