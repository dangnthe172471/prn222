using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_PRN221.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Project_PRN221.Pages.Shop
{
    public class OrderProgressModel : PageModel
    {
        private readonly Project_PRN221.Models.GreenShopContext _context;
        public OrderProgressModel(Project_PRN221.Models.GreenShopContext context)
        {
            _context = context;
        }
        public Order order { get; set; }

        public OrderDetail orderDetail { get; set; }
        public IActionResult OnGet(Guid id)
        {
            order =_context.Orders.Include(o=>o.User).FirstOrDefault(o => o.Id == id);
            orderDetail = _context.OrderDetails.Include(o => o.Product).FirstOrDefault(od => od.OrderId == id);
            this.itemCart = getCart();
            return Page();
        }
        public int? itemCart { get; set; }
        public int getCart()
        {
            Guid? userId = getIdFromToken();
            if (userId != null)
            {
                List<Models.Cart> cart = new List<Models.Cart>();
                cart = _context.Carts.Where(c => c.UserId == userId).ToList();
                return cart.Count;
            }
            else
            {
                return 0;
            }

        }
        public Guid? getIdFromToken()
        {

            var token = Request.Cookies["AuthToken"];

            if (token == null)
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken == null)
                return null;
            // Assuming the GUID is stored in a claim named "id"
            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (Guid.TryParse(idClaim, out Guid guidId))
            {
                return guidId;
            }
            else
            {
                return null;
            }
        }
    }
}
