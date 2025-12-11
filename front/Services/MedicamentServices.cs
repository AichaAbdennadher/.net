using Blazored.LocalStorage;
using metiers;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace front.Services
{
    public class MedicamentServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public MedicamentServices(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<Medicament>> GetMedicaments(string userId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Medicament>>($"api/Medicament/Medicaments/ph/{userId}");
        }

        public async Task<Medicament> GetMedicament(int id)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<Medicament>($"api/Medicament/{id}");
        }

        public async Task<Medicament> CreateMedicament(Medicament Medicament)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Medicament", Medicament);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Medicament>();
        }

        public async Task<bool> UpdateMedicament(Medicament Medicament)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/Medicament", Medicament);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMedicament(int id)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/Medicament/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Medicament> GetMedicamentAsync(int id)
        {
            return await _httpClient.
                GetFromJsonAsync<Medicament>($"api/Medicament/{id}");
        }

    }
}

