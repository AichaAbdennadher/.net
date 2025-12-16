using Blazored.LocalStorage;
using metiers;
using metiers.shared;
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

        public async Task<List<Ordonnance>> GetOrdonnancesEnvoyes(string PharmacienID)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Ordonnance>>($"api/Ordonnance/pharmacy/{PharmacienID}");
        }
        public async Task<List<LigneMedicament>> DelivrerOrdonnance(int ordId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<LigneMedicament>>($"api/Ordonnance/delivree/{ordId}");
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
        public async Task<int> GetNbreOrdonnances(string pharmacienId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<int>(
                $"api/Ordonnance/pharmacy/nbreOrd/{pharmacienId}"
            );
        }
        public async Task<int> GetNbreOrdonnancesLivrees(string pharmacienId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<int>(
                $"api/Ordonnance/pharmacy/nbreOrdLivree/{pharmacienId}"
            );
        }
        public async Task<int> GetNbreOrdonnancesNonLivrees(string pharmacienId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<int>(
                $"api/Ordonnance/pharmacy/nbreOrdNonLivree/{pharmacienId}"
            );
        }
        public async Task<int> GetNbreDoctors(string pharmacienId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<int>(
                $"api/Ordonnance/pharmacy/nbreDoctors/{pharmacienId}"
            );
        }
        public async Task<List<Ordonnance>> GetDernieresOrdonnancesPharmacien(string pharmacienId)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Ordonnance>>($"api/Ordonnance/pharmacien/dernieres/{pharmacienId}");
        }
        public async Task<List<OrdonnanceParMoisDTO>> GetOrdonnancesParMoisPharmacien(
    string pharmacienId, int annee)
        {
            await AddJwtHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<OrdonnanceParMoisDTO>>(
                $"api/Ordonnance/pharmacien/{pharmacienId}/statistiques/mois/{annee}"
            );
        }


    }
}
