﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_PRN221.Models;

namespace Project_PRN221.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly Project_PRN221.Models.GreenShopContext _context;

        public CreateModel(Project_PRN221.Models.GreenShopContext context)
        {
            _context = context;
        }

       
        //int? page, string searchString = null, string roleFilter = null, string statusFilter = null
       
        public List<Role> Roles { get; set; }
        [BindProperty]
        public Project_PRN221.Models.User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string UserName,string Email,string Password,string Dob,string RoleName)
        {

            if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task OnGet()
        {
         
            Roles=await _context.Roles.ToListAsync();
            User=await _context.Users.Include(x=>x.Role).FirstOrDefaultAsync();

        }

    }
}
