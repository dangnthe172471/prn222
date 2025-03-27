using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Project_PRN221.Hubs;
using Project_PRN221.Models;

namespace Project_PRN221.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN221.Models.GreenShopContext _context;

        private readonly IHubContext<ProductHubs> _hubContext;
        public CreateModel(GreenShopContext context, IHubContext<ProductHubs> hubContext)
        {
            this._context = context;
            this._hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            categories = _context.Categories.ToList();
            return Page();
        }
        public List<Models.Category> categories { get; set; }
        [BindProperty]
        public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Products == null || Product == null)
            {
                return OnGet();
            }
            Product.Id = Guid.NewGuid();
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveStudentUpdate", Product.Id, "Created");
            return RedirectToPage("./Index");
        }
    }
}
