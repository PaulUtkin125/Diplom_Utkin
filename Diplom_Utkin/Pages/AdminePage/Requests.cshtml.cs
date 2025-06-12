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

        public Brokers Broker { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        public string CurrnerSort { get; set; }
        public string? sortName { get; set; }
        public string? sortDir { get; set; }
        public string? sortStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(string? sortOrder)
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
            }
            else return Unauthorized();


            CurrnerSort = sortOrder;
            TempData["sortOrder"] = sortOrder;
            sortStatus = string.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            sortName = sortOrder == "name" ? "name_desc" : "name";
            sortDir = sortOrder == "dir" ? "dir_desc" : "dir";
            

            var brokersIQ = await _APIService.NotNewBrokersList();
            switch (sortOrder)
            {
                case "name_desc":
                    brokersIQ = brokersIQ.OrderByDescending(x => x.NameBroker).ToList();
                    break;
                case "name":
                    brokersIQ = brokersIQ.OrderBy(x => x.NameBroker).ToList();
                    break;
                case "dir_desc":
                    brokersIQ = brokersIQ.OrderByDescending(x => x.FullNameOfTheDirector).ToList();
                    break;
                case "dir":
                    brokersIQ = brokersIQ.OrderBy(x => x.FullNameOfTheDirector).ToList();
                    break;

                case "status_desc":
                    brokersIQ = brokersIQ.OrderByDescending(x => x.TypeOfRequestId).ToList();
                    break;
                default:
                    brokersIQ = brokersIQ.OrderBy(x => x.TypeOfRequestId).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(Query))
            {
                brokersIQ = brokersIQ
                    .Where(p => p.NameBroker.StartsWith(Query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Brokers = brokersIQ;

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
            }

            string? sortOrder = (string?)TempData["sortOrder"];
            TempData["sortOrder"] = sortOrder;

            sortName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            sortDir = sortOrder == "dir" ? "dir_desc" : "dir";
            sortStatus = sortOrder == "status" ? "status_desc" : "status";

            var brokersIQ = await _APIService.NotNewBrokersList();

            switch (sortOrder)
            {
                case "name_desc":
                    brokersIQ = brokersIQ.OrderByDescending(x => x.NameBroker).ToList();
                    break;
                case "dir_desc":
                    brokersIQ = brokersIQ.OrderByDescending(x => x.FullNameOfTheDirector).ToList();
                    break;
                case "dir":
                    brokersIQ = brokersIQ.OrderBy(x => x.FullNameOfTheDirector).ToList();
                    break;

                case "status":
                    brokersIQ = brokersIQ.OrderBy(x => x.TypeOfRequestId).ToList();
                    break;
                case "status_desc":
                    brokersIQ = brokersIQ.OrderBy(x => x.TypeOfRequestId).ToList();
                    break;
                default:
                    brokersIQ = brokersIQ.OrderBy(x => x.NameBroker).ToList();
                    break;
            }

            Brokers = brokersIQ;
        }
    }
}
