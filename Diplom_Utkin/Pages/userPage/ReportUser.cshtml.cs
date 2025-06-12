using System.Security.Cryptography.X509Certificates;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Report;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.userPage
{
    public class ReportUserModel : PageModel
    {

        public int uId;
        public User _user { get; set; }
        public double Money { get; set; }

        private readonly APIService _APIService;
       
        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;
        public ReportUserModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }
        [BindProperty]
        public CardModel _cardModel { get; set; } = default!;
        public double targetSumm { get; set; }
        public int isVuvod { get; set; }

        public List<DvizhenieSredstv> InvestToolList { get; set; } = default!;
        public List<Portfolio> porfolioList { get; set; } = default!;
        public ReportTransactionOperatio_Request request { get; set; }

        public string reportTite { get; set; } = "";
        public double[] price { get; set; }
        public double[] negativePrice { get; set; }
        public string[] name { get; set; }
        public string[] name_negative { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime startdataSave { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime enddataSave { get; set; }


        [BindProperty]
        public double? errorSupport { get; set; }
        public async Task<ActionResult> OnGet()
        {
            if (TempData["uId"] != null)
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
                _user = await _APIService.moneyLoadAsync(uId);
                Money = _user.Maney;
                
            }
            else return Unauthorized();



            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? action, DateTime? dateStart, DateTime? dateEnd, int? TypeReport)
        {
            await WorkOfData();
            switch (action)
            {
                case "formatdReport":
                    if (TypeReport == 1)
                    {
                        startdataSave = (DateTime)dateStart;
                        enddataSave = (DateTime)dateEnd;
                        InvestToolList = await _APIService.Report1u((DateTime)dateStart, (DateTime)dateEnd, uId);

                        price = new double[InvestToolList.Count];
                        name = new string[InvestToolList.Count];
                        name_negative = new string[InvestToolList.Count];
                        negativePrice = new double[InvestToolList.Count];
                        for (int i = 0; i < InvestToolList.Count; i++)
                        {
                            if (InvestToolList[i].Money > 0)
                            {
                                price[i] = InvestToolList[i].Quentity * InvestToolList[i].Money;
                                name[i] = InvestToolList[i].InvestTools.NameInvestTool;
                            }
                            else
                            {
                                negativePrice[i] = Math.Abs(InvestToolList[i].Quentity * InvestToolList[i].Money);
                                name_negative[i] = InvestToolList[i].InvestTools.NameInvestTool;
                            }
                            
                        }
                        reportTite = $"о сделках Отчетный период: {dateStart.ToString().Split()[0]} - {dateEnd.ToString().Split()[0]}" ;
                    }
                    else if (TypeReport == 2)
                    {
                        porfolioList = await _APIService.Report2u(uId);
                        reportTite = $"о структуре портфеля На: {DateTime.Now.ToString().Split()[0]}";

                        price = new double[porfolioList.Count];
                        name = new string[porfolioList.Count];
                        double totalSum = porfolioList.Sum(x => x.AllManey);
                        for (int i = 0; i < porfolioList.Count; i++)
                        {
                            price[i] = Math.Round((porfolioList[i].AllManey*100)/ totalSum, 2);
                            name[i] = porfolioList[i].InvestTool.NameInvestTool;
                        }
                    }

                    break;

                case "btnBalans":
                    if (targetSumm > 0)
                    {
                        if (isVuvod == 1)
                        {
                            if (targetSumm > _user.Maney)
                            {
                                errorSupport = 1;
                                return Page();
                            }
                            MoneuUpdate moneuUpdate = new MoneuUpdate()
                            {
                                id = uId,
                                sum = (double)targetSumm,
                                vector = (int)isVuvod
                            };
                            Money = await _APIService.UserUpdateMoneu(moneuUpdate);
                        }
                        else
                        {
                            MoneuUpdate moneuUpdate = new MoneuUpdate()
                            {
                                id = uId,
                                sum = (double)targetSumm,
                                vector = (int)isVuvod
                            };
                            Money = await _APIService.UserUpdateMoneu(moneuUpdate);
                        }
                    }
                    break;
            }
            await WorkOfData();
            return Page();
        }


        private async Task WorkOfData()
        {
            if (TempData["uId"] != null)
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
                _user = await _APIService.moneyLoadAsync(uId);
                Money = _user.Maney;
                
                
            }
        }
    }
}
