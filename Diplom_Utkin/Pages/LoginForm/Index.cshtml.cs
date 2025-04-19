using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.LoginForm_
{
    public class LoginFormModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        public User User { get; set; } = default!;
        public void OnGet()
        {

        }

        public IActionResult OnPost(string action)
        {
            switch (action)
            {
                case "vhod_btn":
                    if(!ModelState.IsValid) return Page();

                    return RedirectToPage("/userPage/Index");

                case "Registr_btn":
                    return RedirectToPage("/LoginForm/SingUp");

                default:
                    return Page();
                
            }
        }
    }
}
