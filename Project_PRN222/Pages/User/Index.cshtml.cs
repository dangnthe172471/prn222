using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN221.Models;

namespace Project_PRN221.Pages.User
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string image {  get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SaveFile(image,"khiem");
            if (string.IsNullOrEmpty(image))
            {
                return OnGet();
            }           

            return RedirectToPage("./Index");
        }
        public async Task SaveFile(string base64String, string id)
        {
            base64String = base64String.Replace("data:image/png;base64,", "");
            //D:\PRN221\Project_PRN221\img\
            if (string.IsNullOrEmpty(base64String))
            {
                return;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img",id+".png");

            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Define the file path

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Write the byte array to a file
            System.IO.File.WriteAllBytes(filePath, imageBytes);
        }
    }
}
