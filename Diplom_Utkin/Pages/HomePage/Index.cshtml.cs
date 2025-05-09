using System.ComponentModel.DataAnnotations;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.dopValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplom_Utkin.Pages.HomePage
{
    public class IndexModel : PageModel
    {
        private readonly APIService _APIService;

        public IndexModel()
        {
            _APIService = new APIService();
        }

        public int idU { get; set; }

        [BindProperty]
        public StartUserData User { get; set; } = default!;
        
        [BindProperty]
        public logoValidationReg logoValidationReg { get; set; } = default!;

        [BindProperty]
        public ConfirmCodeValidation codeValidation { get; set; } = default!;

        public async Task<IActionResult> OnPost(string action)
        {
            switch (action)
            {
                case "vhod_btn":
                    var data = await _APIService.AutorizationAsynk(User);
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

                    User.Login = logoValidationReg.Email;
                    User.Password = logoValidationReg.Password;

                    await _APIService.RegistrationAsync(User);
                    return Page();


                default:
                    return Page();

            }
        }

        public void confSend()
        {
            var comCode = _APIService.ConfirmCode(User.Login).Result;
            if (comCode is null)
            {
                ModelState.AddModelError("confirmError", "Произошел сбой в системе! Поаторите попытку позже");
            }
        }
        
        public void OnGet()
        {
            TempData.Clear();
        }
    }
}
