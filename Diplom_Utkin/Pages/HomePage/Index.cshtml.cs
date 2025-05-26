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
        private readonly IConfiguration _configuration;
        public readonly ApiSettings apiSettings;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            apiSettings = new ApiSettings();
            apiSettings.BaseUrl = _configuration["API:BaseUrl"];
            _APIService = new APIService(apiSettings.BaseUrl);
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
                    User.RoleId = 2;
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
                    User.RoleId = 2;

                    await _APIService.RegistrationAsync(User);
                    return Page();

                case "vhodAdmin_btn":
                    User.RoleId = 1;
                    var data1 = await _APIService.AdminAutorizationAsynk(User);
                    if (data1 != null)
                    {
                        TempData["adminId"] = data1;
                        return RedirectToPage("/AdminePage/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("User.Login", "Неправильный логин или пароль");
                        return Page();
                    }

                case "vhodBroker_btn":
                    User.RoleId = 3;
                    var data2 = await _APIService.BrokerAutorizationAsynk(User);
                    if (data2 != null)
                    {
                        TempData["brokerId"] = data2;
                        return RedirectToPage("/BrokerPage/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("User.Login", "Неправильный логин или пароль");
                        return Page();
                    }
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
