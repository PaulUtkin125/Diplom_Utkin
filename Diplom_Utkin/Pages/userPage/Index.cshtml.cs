using Diplom_Utkin.Model;
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

            var data = _APIService.loadChartAsynk(uId).Result;
            if (data[1] != null)
            {
                salesData = data;
            }
        }
        public IList<Portfolio> InvestToolsList { get; set; } = default!;


        public string sortName { get; set; }
        public string sortSum { get; set; }
        public string CurrnerSort { get; set; }
        public async Task OnGetAsync(int id, string? sortOrder)
        {
            if (id != 0)
            {
                uId = id;
                TempData["uId"] = id;
            }
            else if (TempData["uId"] != null) 
            {
                uId = (int)TempData["uId"];
                TempData["uId"] = uId;
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

            
        }
    }
}
