using metiers.shared;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace front.Services
{
    public class userServices
    {
        private readonly HttpClient httpclient; //pour la communication avec le backend
        private readonly IJSRuntime _js;

        public userServices(HttpClient httpClient)
        {
            this.httpclient = httpClient;
        }

        public async Task<bool> Register(UserDTO newUser)
        {
            var response = await httpclient.PostAsJsonAsync("api/Account/Register", newUser);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var response = await httpclient.PostAsJsonAsync("api/Account/login", loginDTO);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

            // Sauvegarder dans localStorage
            await _js.InvokeVoidAsync("localStorage.setItem", "token", result.token);
            await _js.InvokeVoidAsync("localStorage.setItem", "email", result.email);
            await _js.InvokeVoidAsync("localStorage.setItem", "userId", result.id);

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
