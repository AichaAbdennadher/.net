using Blazored.LocalStorage;
using metiers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace front.Services
{
    public class LigneMedicamentServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public LigneMedicamentServices(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<LigneMedicament> CreateLigneMedicament(LigneMedicament LigneMedicament)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/LigneMedicament", LigneMedicament);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LigneMedicament>();
        }


        public async Task<bool> UpdateLigneMedicament(LigneMedicament LigneMedicament)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/LigneMedicament", LigneMedicament);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLigneMedicament(int id)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/LigneMedicament/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<LigneMedicament>> GetLigneMedicamentsByOrd(int ordID)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<LigneMedicament>>($"api/LigneMedicament/byOrdonnance/{ordID}");
        }

        public async Task<List<LigneMedicament>> GetLigneMedicaments(string userId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<LigneMedicament>>($"api/LigneMedicament/LigneMedicaments/ph/{userId}");
        }

        public async Task<LigneMedicament> GetLigneMedicament(int id)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<LigneMedicament>($"api/LigneMedicament/{id}");
        }

   



    

    }
}


