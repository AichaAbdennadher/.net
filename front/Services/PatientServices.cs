using Blazored.LocalStorage;
using metiers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace front.Services
{
    public class PatientServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public PatientServices(HttpClient httpClient, ILocalStorageService localStorage)
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

        public async Task<List<Patient>> GetPatients(string medecinId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Patient>>($"api/Patient/patients/medecin/{medecinId}");
        }

        public async Task<Patient> GetPatient(int id)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<Patient>($"api/Patient/{id}");
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/Patient", patient);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Patient>();
        }

        public async Task<bool> UpdatePatient(Patient patient)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/Patient", patient);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePatient(int id)
        {
            await AddJwtHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/Patient/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Patient> GetPatientAsync(int id)
        {
            return await _httpClient.
                GetFromJsonAsync<Patient>($"api/Patient/{id}");
        }

    }
}
