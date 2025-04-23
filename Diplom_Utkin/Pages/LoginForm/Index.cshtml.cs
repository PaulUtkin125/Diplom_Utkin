using Diplom_Utkin.Model;
using DiplomAPI.Models.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.LoginForm_
{
    public class LoginFormModel : PageModel
    {
        private readonly APIService _APIService;
        public LoginFormModel() 
        {
            _APIService = new APIService();
        }

        public int idU { get; set; }
        [BindProperty(SupportsGet = true)]
        //public string Email { get; set; }

        
        public StartUserData User { get; set; } = default!;
        public void OnGet()
        {

        }

        public IActionResult OnPost(string action)
        {
            var data = _APIService.AutorizationAsynk(User).Result;
            switch (action)
            {
                case "vhod_btn":
                    if (data != null)
                    {

                        return RedirectToPage("/userPage/Index", new { id = data });
                    }
                    else
                    {
                        ModelState.AddModelError("User.Login", "Неправильный логин или пароль");
                        return Page();
                    }
                    

                case "Registr_btn":
                    return RedirectToPage("/LoginForm/SingUp");

                default:
                    return Page();
                
            }
        }
    }
}
