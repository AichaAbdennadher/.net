using metiers.shared;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;

namespace front.Services
{
    public class userServices
    {
        private readonly HttpClient httpclient; //pour la communication avec le backend
        private readonly IJSRuntime _js;

        public userServices(HttpClient httpClient, IJSRuntime js)
        {
            this.httpclient = httpClient;
            _js = js ?? throw new ArgumentNullException(nameof(js));

        }

        public async Task<bool> Register(UserDTO newUser)
        {
            var response = await httpclient.PostAsJsonAsync("api/Account/Register", newUser);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null) return false;

            var response = await httpclient.PostAsJsonAsync("api/Account/login", loginDTO);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result == null) return false;

            // Sauvegarder dans localStorage
            await _js.InvokeVoidAsync("localStorage.setItem", "token", result.token);
            await _js.InvokeVoidAsync("localStorage.setItem", "email", result.email);
            await _js.InvokeVoidAsync("localStorage.setItem", "userId", result.id);
            await _js.InvokeVoidAsync("localStorage.setItem", "role", result.role);

            return true;
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
            await _js.InvokeVoidAsync("localStorage.removeItem", "email");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userId");
        }
    }
}
