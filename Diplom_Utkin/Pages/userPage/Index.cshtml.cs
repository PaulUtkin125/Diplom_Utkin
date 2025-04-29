using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.userPage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private int uId;
        private readonly APIService _APIService;
        private List<string[]>? salesData { get; set; }
        public double[] chartData { get; set; }
        public string[] chartName { get; set; }


        public IndexModel()
        {
            _APIService = new APIService();
        }

        public IList<Portfolio> InvestToolsList { get; set; } = default!;
        public double Money { get; set; }


        public double? Pribl { get; set; } = 0;

        public string sortName { get; set; }
        public string sortSum { get; set; }
        public string CurrnerSort { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        public DateTime endDate { get; set; }

        public double targetSumm { get; set; }
        public int isVuvod { get; set; }
        public async Task<ActionResult> OnGetAsync(int id, string? sortOrder, string? action, double? targetSumm, int? isVuvod, int? vector,
            DateTime? startDate, DateTime? endDate)
        {

            if (id != 0)
            {
                uId = id;
                TempData["uId"] = id;
                Money = _APIService.moneyLoadAsync(uId).Result;

                var data = _APIService.loadChartAsynk(uId).Result;
                if (data[0][1] != null)
                {
                    chartData = new double[data.Count];
                    chartName = new string[data.Count];
                    for (int i = 0; i < data.Count; i++)
                    {
                        chartData[i] = double.Parse(data[i][1]);
                        chartName[i] = data[i][0];
                    }
                    salesData = data;
                }
            }
            else if (TempData["uId"] != null) 
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
                Money = _APIService.moneyLoadAsync(uId).Result;
                var data = _APIService.loadChartAsynk(uId).Result;
                if (data[0][1] != null)
                {
                    chartData = new double[data.Count];
                    chartName = new string[data.Count];
                    for (int i = 0; i < data.Count; i++)
                    {
                        chartData[i] = double.Parse(data[i][1]);
                        chartName[i] = data[i][0];
                    }
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

                case "raschot_Btn":

                    var resalt = await _APIService.CalkulateAsync(startDate, endDate, uId);
                    if (resalt == null)
                    {
                        ModelState.AddModelError("Pribl", "Даные отсутствуют!");
                        break;
                    }
                    Pribl = resalt;
                    break;
            }

            return Page();
        }
    }
}
