using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.HomePage
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            TempData.Clear();
        }
    }
}
