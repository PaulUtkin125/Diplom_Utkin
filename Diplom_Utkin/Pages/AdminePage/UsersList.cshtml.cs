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

        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["adminId"];
                TempData["adminId"] = adminId;
                _user = await _APIService.moneyLoadAsync(adminId);
                Users = await _APIService.AllUserList(adminId);
            }
            else return Unauthorized();


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
                Users = await _APIService.AllUserList(adminId);
            }
        }
    }
}