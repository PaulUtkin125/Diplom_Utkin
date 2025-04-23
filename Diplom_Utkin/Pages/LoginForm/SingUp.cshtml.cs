using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finansu.Model;
using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model;
using DiplomAPI.Models.Support;

namespace Diplom_Utkin.Pages.LoginForm
{
    public class SingUpModel : PageModel
    {
        private readonly APIService _APIService;

        public SingUpModel()
        {
            _APIService = new APIService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StartUserData User { get; set; } = default!;


        [BindProperty]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [MinLength(8, ErrorMessage = "Минимальная длина 8 симвалов")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (action == "Beck_btn") return RedirectToPage("/LoginForm/Index");
            if (User == null)
            {
                return Page();

            }
            string pasword = User.Password.Trim();
            string reppitPasword = ConfirmPassword.Trim();

            
            if(pasword != reppitPasword)
            {
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
                ModelState.AddModelError("User.Password", "Пароли не совпадают");
                return Page();
            }
            if(!_APIService.RegistrationAsync(User).Result)
            {
                ModelState.AddModelError("User.Login", "Такой пользователь уже существует");
                return Page();
            }
            else return RedirectToPage("./Index");
        }
    }
}
