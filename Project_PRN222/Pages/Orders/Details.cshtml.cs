﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN221.Models;

namespace Project_PRN221.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly Project_PRN221.Models.GreenShopContext _context;

        public DetailsModel(Project_PRN221.Models.GreenShopContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!; 
        public List<OrderDetail> Details { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }
            if (_context.OrderDetails != null) {
                Details = await _context.OrderDetails.Where(x=>x.OrderId==id).Include(x=>x.Product).ToListAsync();
            }
            else
            {
                Details=new List<OrderDetail>();
            }
            
            return Page();
        }
    }
}
