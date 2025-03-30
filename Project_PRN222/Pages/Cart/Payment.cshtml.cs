﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_PRN221.Models;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;

namespace Project_PRN221.Pages.Shop
{
    public class PaymentModel : PageModel
    {
        private readonly Project_PRN221.Models.GreenShopContext _context;

        public PaymentModel(Project_PRN221.Models.GreenShopContext context)
        {
            _context = context;
        }
        public string url {  get; set; }
        public IActionResult OnGet(double price,int method)
        {
            this.itemCart = getCart();
            Guid? userId = getIdFromToken();
            Models.User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            ShipmentDetail detail = _context.ShipmentDetails.FirstOrDefault(x=>x.UserId==userId);
            List<Models.Cart> carts = _context.Carts.Include(x => x.Product).Where(x => x.UserId == userId).ToList();
            Guid paymentId=Guid.NewGuid(); 
            //add order and delete cart
            if (user != null && detail != null && carts.Count>0) {

                Order order = new Order();
                order.Id=Guid.NewGuid();
                paymentId = order.Id;
                order.TotalPrice = (float)price;
                order.Status = 0;
                order.UserId = (Guid)userId;
                order.Note = "User:" + userId + " Type:" + (method == 1 ? "Banking" : "COD"); ;
                order.ShipmentDetailsId = detail.Id;
                order.CreateAt = DateTime.Now;
                try
                {
                    //add order
                    _context.Orders.Add(order);
                    //get cart
                    foreach (var cart in carts)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.Id = Guid.NewGuid();
                        orderDetail.OrderId = order.Id  ;
                        orderDetail.ProductId = cart.ProductId;
                        orderDetail.Quantity = cart.Quantity;
                        orderDetail.Price = cart.Product.Price;
                        //add order detail
                        _context.OrderDetails.Add(orderDetail);
                    }
                    //delete cart
                    _context.Carts.RemoveRange(carts);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            
            }
            if (method == 1)
            {
                var apiRequest = new ApiRequest
                {
     //               
					//accountNo = "03201012538676",
					accountNo = "0962900476",
					accountName = "NGUYEN TIEN DANG",
					acqId = 970422,
					//acqId = 970426,
					amount = (int)price,
                    addInfo = user == null ? "" : " " + user.Username.ToUpper() + " - " + detail.PhoneNumber + " chuyen khoan EcoNest Đến anh Đăng đẹp trai",
                    format = "text",
                    template = "compact"
                };

                var jsonRequest = JsonConvert.SerializeObject(apiRequest);
                var client = new RestClient("https://api.vietqr.io/v2/generate");
                var request = new RestRequest
                {
                    Method = RestSharp.Method.Post
                };
                request.AddHeader("Accept", "application/json");
                request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
                var responseE = client.Execute(request);
                var content = responseE.Content;
                //data result
                var dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);
                this.url= dataResult.data.qrDataURL;
            }
            else
            {
                
            }
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
