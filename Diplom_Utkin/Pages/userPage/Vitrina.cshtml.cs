using System;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Configuration;

namespace Diplom_Utkin.Pages.userPage
{
    public class VitrinaModel : PageModel
    {
        public int uId;
        public User _user { get; set; }
        public double Money { get; set; }

        private readonly APIService _APIService;
        public List<InvestTools> AllInvestToolsList { get; set; } = default!;

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;
        public VitrinaModel(IConfiguration configuration)
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

        public string sortName { get; set; }
        public string sortBroker { get; set; }
        public string CurrnerSortAll { get; set; }
        public async Task<ActionResult> OnGetAsync(string? sortOrderAll, string? action, double? targetSumm, int? isVuvod, int? vector)
        {
            if (TempData["uId"] is null) { 
                return Unauthorized();
            }

            uId = (int)TempData["uId"];
            TempData["uId"] = uId;
            _user = await _APIService.moneyLoadAsync((int)uId);
            Money = _user.Maney;



            CurrnerSortAll = sortOrderAll;
            TempData["sortOrderAll"] = sortOrderAll;
            sortName = string.IsNullOrEmpty(sortOrderAll) ? "name_desc" : "";
            sortBroker = sortOrderAll == "broker" ? "broker_desc" : "broker";
            var investToolsAllIQ = await _APIService.LoadAllToolsAsinc();
            investToolsAllIQ = investToolsAllIQ.Where(x => x.isClosed == false).ToList();

            switch (sortOrderAll)
            {
                case "name_desc":
                    investToolsAllIQ = investToolsAllIQ.OrderByDescending(x => x.NameInvestTool).ToList();
                    break;

                case "broker_desc":
                    investToolsAllIQ = investToolsAllIQ.OrderByDescending(x => x.Brokers.NameBroker).ToList();
                    break;

                case "broker":
                    investToolsAllIQ = investToolsAllIQ.OrderBy(x => x.Brokers.NameBroker).ToList();
                    break;

                default:
                    investToolsAllIQ = investToolsAllIQ.OrderBy(x => x.NameInvestTool).ToList();
                    break;
            }

            AllInvestToolsList = investToolsAllIQ;



            switch (action)
            {
                case "btnBalans":
                    if (targetSumm > 0)
                    {
                        if (isVuvod == 1)
                        {
                            MoneuUpdate moneuUpdate = new MoneuUpdate()
                            {
                                id = (int)uId,
                                sum = (double)targetSumm,
                                vector = (int)isVuvod
                            };
                            Money = await _APIService.UserUpdateMoneu(moneuUpdate);
                        }
                        else
                        {
                            MoneuUpdate moneuUpdate = new MoneuUpdate()
                            {
                                id = (int)uId,
                                sum = (double)targetSumm,
                                vector = (int)isVuvod
                            };
                            Money = await _APIService.UserUpdateMoneu(moneuUpdate);
                        }
                    }
                break;
            }
            
            return Page();
            
        }

        public async Task<ActionResult> OnPost(string? action, double? targetSumm, int? isVuvod)
        {
            await WorkOfData();

            switch (action)
            {
                case "btnBalans":
                    if (targetSumm > 0)
                    {
                        if (isVuvod == 1)
                        {
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
                _user = _APIService.moneyLoadAsync((int)uId).Result;
                Money = _user.Maney;
                
                string? sortOrderAll = (string?)TempData["sortOrderAll"];
                TempData["sortOrder"] = sortOrderAll;
                sortName = string.IsNullOrEmpty(sortOrderAll) ? "name_desc" : "";
                sortBroker = sortOrderAll == "broker" ? "broker_desc" : "broker";

                var investToolsAllIQ = await _APIService.LoadAllToolsAsinc();


                switch (sortOrderAll)
                {
                    case "name_desc":
                        investToolsAllIQ = investToolsAllIQ.OrderByDescending(x => x.NameInvestTool).ToList();
                        break;

                    case "broker_desc":
                        investToolsAllIQ = investToolsAllIQ.OrderByDescending(x => x.Brokers.NameBroker).ToList();
                        break;

                    case "broker":
                        investToolsAllIQ = investToolsAllIQ.OrderBy(x => x.Brokers.NameBroker).ToList();
                        break;

                    default:
                        investToolsAllIQ = investToolsAllIQ.OrderBy(x => x.NameInvestTool).ToList();
                        break;
                }

                AllInvestToolsList = investToolsAllIQ;
            }
        }

    }
}
