using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN221.Models;

namespace Project_PRN221.Pages.Login
{
    public class GoogleResponseModel : PageModel
    {
        public IActionResult OnGet()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("LoginWithGoogle", null, null, Request.Scheme)
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            

            return RedirectToPage("");
        }
    }
}
