using Diplom_Utkin.Model.dopValidation;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.Extensions.Configuration;

namespace Diplom_Utkin.Model.Data
{
    public class APIService
    {
        private HttpClient _httpClient;
        public APIService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5189/api/");
        }

        public async Task<int?> ConfirmCode(string mail)
        {
            try
            {
                var processPositive = await _httpClient.PostAsJsonAsync("Universal/SendCode", mail);
                var resalt = await processPositive.Content.ReadFromJsonAsync<int>();
                return resalt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> moneyLoadAsync(int id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<User>();
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
        public async Task<int?> AdminAutorizationAsynk(StartUserData user)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("Logo/AutorisationAdmin", user);
            int? resalt;
            if (processPositive.IsSuccessStatusCode)
            {
                resalt = await processPositive.Content.ReadFromJsonAsync<int?>();
                return resalt;
            }
            return null;
        }

        public async Task<List<string[]>?> loadChartAsynk(int id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User/loadCart", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<List<string[]>?>();
            if (resalt == null) return null;
            return resalt;
        }

        public async Task<List<Portfolio>?> LoadUsersToolsAsinc(int id)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("User/UserSTools", id);
            var resalt = await processPositive.Content.ReadFromJsonAsync<List<Portfolio>?>();
            if (resalt == null) return null;
            else return resalt;
        }
        public async Task<List<InvestTools>?> LoadAllToolsAsinc()
        {
            var processPositive = await _httpClient.GetAsync("User/allInvestTool");
            var resalt = await processPositive.Content.ReadFromJsonAsync<List<InvestTools>>();
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
            var processPositive = await _httpClient.PostAsJsonAsync("User/Calculate", calculateSupport);
            var resalt = await processPositive.Content.ReadFromJsonAsync<double>();
            if (resalt == 0.0) return null;
            else return resalt;
        }



















        public async Task<List<Brokers>> newBrokersAsync()
        {
            var BrokerList = await _httpClient.GetAsync("Admin/NewBrokersList");
            var resalt = await BrokerList.Content.ReadFromJsonAsync<List<Brokers>>();
            return resalt;
        }
        public async Task<List<Brokers>> NotNewBrokersList()
        {
            var BrokerList = await _httpClient.GetAsync("Admin/NotNewBrokersList");
            var resalt = await BrokerList.Content.ReadFromJsonAsync<List<Brokers>>();
            return resalt;
        }


        public async Task ModefiteRequestAsync(ModefiteRequestSupport modefite)
        {
            var processPositive = await _httpClient.PostAsJsonAsync("Admin/ModefiteRequest", modefite);
            
        }


        public async Task<List<User>> AllUserList(int id)
        {
            var BrokerList = await _httpClient.PostAsJsonAsync("Admin/AllUserlist", id);
            var resalt = await BrokerList.Content.ReadFromJsonAsync<List<User>>();
            return resalt;
        }

        public async Task deleteUser(int id)
        {
            var processPositive = await _httpClient.PatchAsJsonAsync("Admin/deleteUser", id);
        }
    }
}
