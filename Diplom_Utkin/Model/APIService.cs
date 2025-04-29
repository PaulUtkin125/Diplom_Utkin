using Diplom_Utkin.Model.Support;
using DiplomAPI.Models.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Authorization;

namespace Diplom_Utkin.Model
{
    public class APIService
    {
        private HttpClient _httpClient;
        public APIService() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5189/api/");
        }

        public async Task<double> moneyLoadAsync(int id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<double>();
            return resalt;
        }

        public async Task<double> UserUpdateMoneu(MoneuUpdate moneuUpdate)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User/updateMoneu", moneuUpdate);
            var resalt = await processPositive.Content.ReadFromJsonAsync<double>();
            return resalt;
        }












        public async Task<bool> RegistrationAsync(StartUserData user)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("Logo/UserAdd", user);
            bool registrationResalt = await processPositive.Content.ReadFromJsonAsync<bool>();
            if (registrationResalt) return true;
            return false;
        }

        public async Task<string?> SupportFromToken(StartUserData user)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("Logo/Autorisation", user);
            string? resalt;
            if (processPositive.IsSuccessStatusCode)
            {
                resalt = await processPositive.Content.ReadAsStringAsync();
                return resalt;
            }
            return null;
        }
        public async Task<int?> AutorizationAsynk(StartUserData user)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("Logo/Autorisation", user);
            int? resalt;
            if (processPositive.IsSuccessStatusCode)
            {
                resalt = await processPositive.Content.ReadFromJsonAsync<int?>();
                return resalt;
            }
            return null;
        }
        public async Task<List<string[]>?> loadChartAsynk(int  id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User/loadCart", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<List<string[]>?>();
            if (resalt == null) return null;
            return resalt;
        }

        public async Task<List<Portfolio>?> LoadUsersToolsAsinc(int id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User/UserSTools", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<List<Portfolio>>();
            if (resalt == null) return null;
            else return resalt;
        }



        public async Task<double?> CalkulateAsync(DateTime? startDate, DateTime? endDate, int id)
        {
            CalculateSupport calculateSupport = new()
            {
                Id = id,
                dateFinish = endDate,
                dateStart = startDate,
            };
            var processPositive = await _httpClient.PostAsJsonAsync($"User/Calculate", calculateSupport);
            var resalt = await processPositive.Content.ReadFromJsonAsync<double>();
            if (resalt == 0.0) return null;
            else return resalt;
        }
    }
}
