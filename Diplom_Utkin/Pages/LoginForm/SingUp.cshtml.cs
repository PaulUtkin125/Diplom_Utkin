using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Finansu.Data;
using Finansu.Model;

namespace Diplom_Utkin.Pages.LoginForm
{
    public class SingUpModel : PageModel
    {
        private readonly Finansu.Data.dbContact _context;

        public SingUpModel(Finansu.Data.dbContact context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.User == null || User == null)
            {
                return Page();
            }
            string pasword = User.PaswordHash.Trim();
            string reppitPasword = ConfirmPassword.Trim();

            if (pasword.Length < 8) 
            {
                return Page();
            }
            if(pasword != reppitPasword)
            {
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
