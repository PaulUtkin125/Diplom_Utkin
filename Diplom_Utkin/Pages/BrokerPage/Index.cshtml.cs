using System.Security.Cryptography.X509Certificates;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.BrokerPage
{
    public class IndexModel : PageModel
    {
        public int brokerId;
        private readonly APIService _APIService;
        public Brokers _user { get; set; }

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }

        public List<InvestTools> InvestTools { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            if (TempData["brokerId"] != null)
            {
                brokerId = (int)TempData["brokerId"];
                TempData["brokerId"] = brokerId;
                _user = await _APIService.LoadBrokerDataAsync(brokerId);
                InvestTools = await _APIService.InvestListAsync(brokerId);
            }
            else return Unauthorized();

            return Page();
        }

        public async Task<ActionResult> OnPost(string? action)
        {
            await WorkOfData();

            

            return Page();
        }

        private async Task WorkOfData()
        {
            if (TempData["brokerId"] != null)
            {
                brokerId = (int)TempData["brokerId"];
                TempData["brokerId"] = brokerId;
                _user = await _APIService.LoadBrokerDataAsync(brokerId);
                InvestTools = await _APIService.InvestListAsync(brokerId);
            }
        }
    }
}
