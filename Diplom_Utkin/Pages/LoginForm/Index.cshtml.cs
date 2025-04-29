using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Diplom_Utkin.Model;
using DiplomAPI.Models.Support;
using Finansu.Model;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using System.Configuration;

namespace Diplom_Utkin.Pages.LoginForm_
{
    public class LoginFormModel : PageModel
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        public string jwtToken { get; set; }
        private readonly APIService _APIService;

        public LoginFormModel(IConfiguration configuration) 
        {
            _APIService = new APIService();

            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
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
            switch (action)
            {
                case "vhod_btn":
                    var data = _APIService.AutorizationAsynk(User).Result;
                    if (data != null)
                    {
                        // Генерация токена
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, data.ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(_issuer, 
                                                          _audience, claims, 
                                                          expires: DateTime.Now.AddMinutes(30), 
                                                          signingCredentials: creds);

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        HttpContext.Session.SetString("jwtToken", tokenString);
                        //var tr = HttpContext.Session.GetString("jwt");

                        Console.WriteLine($"Token added to cookie: {tokenString}");
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
