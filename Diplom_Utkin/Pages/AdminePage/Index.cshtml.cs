using Diplom_Utkin.Model.Data;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.AdinePage
{

    public class IndexModel : PageModel
    {
        public int adminId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["adminId"] != null)
            {
                adminId = (int)TempData["uId"];
                TempData["adminId"] = adminId;
               
            }
            else return Unauthorized();

            return Page();
        }
    }
}
