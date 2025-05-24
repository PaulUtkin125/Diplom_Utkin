using System.Configuration;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.AdminePage
{
    public class RequestsModel : PageModel
    {
        public int adminId { get; set; }
        public User _user { get; set; }
        public List<Brokers> Brokers { get; set; }

        private readonly APIService _APIService;

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;

        public RequestsModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }


        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
                Brokers = await _APIService.NotNewBrokersList();
            }
            else return Unauthorized();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? action, int? brokerId, int? TargetbrokerId)
        {
            if (brokerId == null && TargetbrokerId != null) brokerId = TargetbrokerId;
            switch (action)
            {
                case "positive_btn":
                    ModefiteRequestSupport modefite = new()
                    {
                        brokerId = (int)brokerId,
                        mode = 0
                    };
                    await _APIService.ModefiteRequestAsync(modefite);
                    break;

                case "negative_btn":
                    ModefiteRequestSupport modefite1 = new()
                    {
                        brokerId = (int)brokerId,
                        mode = 1
                    };
                    await _APIService.ModefiteRequestAsync(modefite1);
                    break;
            }
            await WorkOfData();
            return Page();
        }

        private async Task WorkOfData()
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
                Brokers = await _APIService.NotNewBrokersList();
            }
        }
    }
}
