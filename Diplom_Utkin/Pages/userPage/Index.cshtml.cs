using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.userPage
{
    public class IndexModel : PageModel
    {
        private int uId;
        private readonly APIService _APIService;
        public List<string[]>? salesData { get; set; }
        public IndexModel()
        {
            _APIService = new APIService();

            

            
        }
        public IList<Portfolio> InvestToolsList { get; set; } = default!;
        //public User _user { get; set; }
        public double Money { get; set; }

        public string sortName { get; set; }
        public string sortSum { get; set; }
        public string CurrnerSort { get; set; }


        [BindProperty]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Сумма должна быть больше нуля!")]
        public double targetSumm { get; set; }
        
        public int isVuvod { get; set; }
        public async Task OnGetAsync(int id, string? sortOrder, string? actionBalens, double? targetSumm, int? isVuvod, int? vector)
        {
            if (id != 0)
            {
                uId = id;
                TempData["uId"] = id;
                Money = _APIService.moneyLoadAsync(uId).Result;

                var data = _APIService.loadChartAsynk(uId).Result;
                if (data[1] != null)
                {
                    salesData = data;
                }
            }
            else if (TempData["uId"] != null) 
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
                Money = _APIService.moneyLoadAsync(uId).Result;
                var data = _APIService.loadChartAsynk(uId).Result;
                if (data[1] != null)
                {
                    salesData = data;
                }
            }

            CurrnerSort = sortOrder;
            sortName = string.IsNullOrEmpty(sortOrder)? "name_desc" : "";
            sortSum = sortOrder == "sum" ? "sum_desc" : "sum";

            var investToolsIQ = await _APIService.LoadUsersToolsAsinc(uId);


            switch (sortOrder)
            {
                case "name_desc":
                    investToolsIQ = investToolsIQ.OrderByDescending(x => x.InvestTool.NameInvestTool).ToList();
                    break;

                case "sum_desc":
                    investToolsIQ = investToolsIQ.OrderByDescending(x => x.AllManey).ToList();
                    break;

                case "sum":
                    investToolsIQ = investToolsIQ.OrderBy(x => x.AllManey).ToList();
                    break;

                default:
                    investToolsIQ = investToolsIQ.OrderBy(x => x.InvestTool.NameInvestTool).ToList();
                    break;
            }

            InvestToolsList = investToolsIQ;

            if (actionBalens == "btnBalans")
            {
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
            }
            

        }
    }
}
