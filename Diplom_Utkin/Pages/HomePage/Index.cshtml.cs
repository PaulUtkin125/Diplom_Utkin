using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Diplom_Utkin.Model.Data;
using Diplom_Utkin.Model.dopValidation;
using Diplom_Utkin.Model.Support;
using Finansu.Model;
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
        public StartUserData user { get; set; }

        [BindProperty]
        public logoValidationReg logoValidationReg { get; set; }

        [BindProperty]
        public ConfirmCodeValidation codeValidation { get; set; } = default!;
       
        [BindProperty]
        public Brokers Broker { get; set; }

        [BindProperty]
        public double? errorSupport { get; set; }
        public async Task<IActionResult> OnPost(string? action)
        {
            switch (action)
            {
                case "vhod_btn":
                    user.RoleId = 2;
                    try
                    {
                        var data = await _APIService.AutorizationAsynk(user);
                        if (data != null)
                        {
                            TempData["uId"] = data;
                            return RedirectToPage("/userPage/Index");
                        }
                        else
                        {
                            errorSupport = 1;
                            return Page();
                        }
                    }
                    catch (Exception)
                    {
                        errorSupport = 1.1;
                        return Page();
                    }


                case "Registr_btn":
                    
                    user.Login = logoValidationReg.Email;
                    user.Password = logoValidationReg.Password;
                    user.RoleId = 2;

                    await _APIService.RegistrationAsync(user);
                    errorSupport = 4.1;
                    return Page();


                case "vhodAdmin_btn":
                    user.RoleId = 1;

                    try
                    {
                        var data1 = await _APIService.AdminAutorizationAsynk(user);
                        if (data1 != null)
                        {
                            TempData["adminId"] = data1;
                            return RedirectToPage("/AdminePage/Requests");
                        }
                        else
                        {
                            errorSupport = 2;
                            return Page();
                        }
                    }
                    catch (Exception)
                    {
                        errorSupport = 2.1;
                        return Page();
                    }
                    
                ;

                case "vhodBroker_btn":
                    user.RoleId = 3;

                    try
                    {
                        var data2 = await _APIService.BrokerAutorizationAsynk(user);
                        if (data2 != null)
                        {
                            TempData["brokerId"] = data2;
                            return RedirectToPage("/BrokerPage/Index");
                        }
                        else
                        {
                            errorSupport = 3;
                            return Page();
                        }
                    }
                    catch (Exception)
                    {
                        errorSupport = 3.1;
                        return Page();
                    }
                   
                ;

                default:
                    return Page();

            }
        }


        public void OnGet()
        {
            TempData.Clear();
            ModelState.Clear();
        }
    }
}
