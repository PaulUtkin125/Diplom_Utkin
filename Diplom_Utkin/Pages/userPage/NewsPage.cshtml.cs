using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.userPage
{
    public class NewsPageModel : PageModel
    {
        public int uId;
        private readonly APIService _APIService;
        

        public User _user { get; set; }
        public double targetSumm { get; set; }
        public int isVuvod { get; set; }

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;
        public NewsPageModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }

        public IList<Portfolio> InvestToolsList { get; set; } = default!;
        public List<InvestTools> AllInvestToolsList { get; set; }
        public double Money { get; set; }


        [BindProperty]
        public CardModel _cardModel { get; set; } = default!;

        public double? Pribl { get; set; } = 0;

        public string sortName { get; set; }
        public string sortSum { get; set; }
        public string CurrnerSort { get; set; }
        public async Task<ActionResult> OnGet()
        {
            if (TempData["uId"] != null)
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
                _user = await _APIService.moneyLoadAsync(uId);
                Money = _user.Maney;

            }
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
