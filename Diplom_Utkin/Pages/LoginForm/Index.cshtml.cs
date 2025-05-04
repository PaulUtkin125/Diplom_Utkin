using System.Security.Claims;
using System;
using Diplom_Utkin.Model;
using DiplomAPI.Models.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using System.Configuration;

namespace Diplom_Utkin.Pages.LoginForm_
{
    public class LoginFormModel : PageModel
    {
        private readonly APIService _APIService;

        public LoginFormModel(IConfiguration configuration) 
        {
            _APIService = new APIService();
        }

        public int idU { get; set; }

        [BindProperty]
        public StartUserData User { get; set; } = default!;
        public void OnGet()
        {

        }

        public IActionResult OnPost(string action)
        {
            switch (action)
            {
                case "vhod_btn":
                    var data = _APIService.AutorizationAsynk(User).Result;
                    if (data != null)
                    {
                        TempData["uId"] = data;
                        return RedirectToPage("/userPage/Index");
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
