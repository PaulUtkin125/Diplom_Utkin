using System.Security.Cryptography.X509Certificates;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Configuration;

namespace Diplom_Utkin.Pages.AdminePage
{
    public class UsersListModel : PageModel
    {
        public int adminId { get; set; }
        public User _user { get; set; }

        [BindProperty]
        public List<User> Users { get; set; }

        private readonly APIService _APIService;

        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;
        public UsersListModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
        }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        public string CurrnerSort { get; set; }
        public string? sortMail { get; set; }
        public string? sortPhone { get; set; }

        public async Task<IActionResult> OnGetAsync(string? sortOrder)
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
                //Users = await _APIService.AllUserList(adminId);
            }
            else return Unauthorized();

            CurrnerSort = sortOrder;
            TempData["sortOrder"] = sortOrder;
            sortMail = string.IsNullOrEmpty(sortOrder) ? "mail_desc" : "";
            sortPhone = sortOrder == "phone" ? "phone_desc" : "phone";

            var usersIQ  = await _APIService.AllUserList(adminId);

            switch (sortOrder)
            {
                case "mail_desc":
                    usersIQ = usersIQ.OrderByDescending(x => x.Loggin).ToList();
                    break;
                case "phone_desc":
                    usersIQ = usersIQ.OrderByDescending(x => x.Phone).ToList();
                    break;
                case "phone":
                    usersIQ = usersIQ.OrderBy(x => x.Phone).ToList();
                    break;
                default:
                    usersIQ = usersIQ.OrderBy(x => x.Loggin).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(Query))
            {
                usersIQ = usersIQ
                    .Where(p => p.Loggin.StartsWith(Query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Users = usersIQ;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string? action, int? userId, int? TargetUserId)
        {
            if (userId == null && TargetUserId != null) userId = TargetUserId;
            switch (action)
            {

                case "delete_btn":
                    await _APIService.deleteUser((int)userId);
                    break;
            }
            Query = Query;
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

                string? sortOrder = (string?)TempData["sortOrder"];
                TempData["sortOrder"] = sortOrder;
                sortMail = string.IsNullOrEmpty(sortOrder) ? "mail_desc" : "";
                sortPhone = sortOrder == "phone" ? "phone_desc" : "phone";

                var usersIQ = await _APIService.AllUserList(adminId);

                switch (sortOrder)
                {
                    case "mail_desc":
                        usersIQ = usersIQ.OrderByDescending(x => x.Loggin).ToList();
                        break;
                    case "phone_desc":
                        usersIQ = usersIQ.OrderByDescending(x => x.Phone).ToList();
                        break;
                    case "phone":
                        usersIQ = usersIQ.OrderBy(x => x.Phone).ToList();
                        break;
                    default:
                        usersIQ = usersIQ.OrderBy(x => x.Loggin).ToList();
                        break;
                }

                Users = usersIQ;
            }
        }
    }
}