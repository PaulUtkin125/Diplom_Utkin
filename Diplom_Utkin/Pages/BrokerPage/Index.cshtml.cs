using System.Security.Cryptography.X509Certificates;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.dopValidation;
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

        public CreateToolValidation investTool { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        public string CurrnerSort { get; set; }
        public string? sortName { get; set; }
        public string? sortIsClosing { get; set; }
        public async Task<ActionResult> OnGetAsync(string? sortOrder)
        {
            if (TempData["brokerId"] != null)
            {
                brokerId = (int)TempData["brokerId"];
                TempData["brokerId"] = brokerId;
                _user = await _APIService.LoadBrokerDataAsync(brokerId);
                
            }
            else return Unauthorized();


            CurrnerSort = sortOrder;
            TempData["sortOrder"] = sortOrder;
            sortName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            sortIsClosing = sortOrder == "status" ? "status_desc" : "status";

            var toolIQ = await _APIService.InvestListAsync(brokerId);
            switch (sortOrder)
            {
                case "name_desc":
                    toolIQ = toolIQ.OrderByDescending(x => x.NameInvestTool).ToList();
                    break;
                case "status":
                    toolIQ = toolIQ.OrderBy(x => x.isClosed).ToList();
                    break;
                case "status_desc":
                    toolIQ = toolIQ.OrderByDescending(x => x.isClosed).ToList();
                    break;

                default:
                    toolIQ = toolIQ.OrderBy(x => x.NameInvestTool).ToList();
                    break;
            }

            InvestTools = toolIQ;
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
            }

            string? sortOrder = (string?)TempData["sortOrder"];
            TempData["sortOrder"] = sortOrder;

            sortName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            sortIsClosing = sortOrder == "status" ? "status_desc" : "status";

            var toolIQ = await _APIService.InvestListAsync(brokerId);
            switch (sortOrder)
            {
                case "name_desc":
                    toolIQ = toolIQ.OrderByDescending(x => x.NameInvestTool).ToList();
                    break;
                case "status":
                    toolIQ = toolIQ.OrderBy(x => x.isClosed).ToList();
                    break;
                case "status_desc":
                    toolIQ = toolIQ.OrderByDescending(x => x.isClosed).ToList();
                    break;

                default:
                    toolIQ = toolIQ.OrderBy(x => x.NameInvestTool).ToList();
                    break;
            }

            InvestTools = toolIQ;
        }
    }
}
