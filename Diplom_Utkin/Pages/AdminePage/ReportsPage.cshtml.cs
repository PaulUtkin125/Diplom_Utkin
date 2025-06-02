using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Report;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.AdminePage
{
    public class ReportsPageModel : PageModel
    {
        public int adminId { get; set; }
        public User _user { get; set; }
        public Report1Reauest report1Reauest { get; set; }

        public List<Repport1Model> repport1Model { get; set; } = default!;
        public List<AltBrokers> report2Models { get; set; } = default!;
        public List<Brokers> Brokers { get; set; }

        private readonly APIService _APIService;

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;

        public ReportsPageModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }

        public string reportTite { get; set; } = "";
        public double[] price { get; set; }
        public double[] negativePrice { get; set; }
        public string[] name { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime startdataSave { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime enddataSave { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
            }
            else return Unauthorized();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? action, DateTime? dateStart, DateTime? dateEnd, int? TypeReport)
        {
            
            switch (action)
            {
                case "formatdReport":
                    startdataSave = (DateTime)dateStart;
                    enddataSave = (DateTime)dateEnd;

                    if (TypeReport == 1)
                    {
                        repport1Model = await _APIService.Report1((DateTime)dateStart, (DateTime)dateEnd, (int)TypeReport);

                        price = new double[repport1Model.Count];
                        name = new string[repport1Model.Count];
                        negativePrice = new double[repport1Model.Count];
                        for (int i = 0; i < repport1Model.Count; i++)
                        {
                            price[i] = repport1Model[i].sumPokupokClientov;
                            negativePrice[i] = repport1Model[i].sumProdazhClientov;
                            name[i] = repport1Model[i].BrokerName;

                            reportTite = "по обороту денежных средств(по партнерам)";
                        }
                    }
                    else if (TypeReport == 2) 
                    {
                        report2Models = await _APIService.Report2((DateTime)dateStart, (DateTime)dateEnd, (int)TypeReport);
                        reportTite = "по заявкам на регистрацию брокеров";
                    }
                   
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
        }
    }
}
