using Blazored.LocalStorage;
using metiers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace front.Services
{
    public class OrdonnanceServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public OrdonnanceServices(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Ajoute automatiquement le JWT
        private async Task AddJwtHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<List<Ordonnance>> GetOrdonnancesByMedecin(string medecinId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Ordonnance>>($"api/Ordonnance/medecin/{medecinId}");
        }

        public async Task<Ordonnance> GetOrdonnance(int id)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<Ordonnance>($"api/Ordonnance/{id}");
        }

        public async Task<Ordonnance> CreateOrdonnance(Ordonnance Ordonnance)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Ordonnance", Ordonnance);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Ordonnance>();
        }

        public async Task<bool> UpdateOrdonnance(Ordonnance Ordonnance)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/Ordonnance/update", Ordonnance);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendOrdonnance(Ordonnance Ordonnance)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/Ordonnance/envoyer", Ordonnance);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrdonnance(int id)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/Ordonnance/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
