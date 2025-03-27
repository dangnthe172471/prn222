using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Comments = new HashSet<Comment>();
            News = new HashSet<News>();
            Orders = new HashSet<Order>();
            ShipmentDetails = new HashSet<ShipmentDetail>();
            UserVouchers = new HashSet<UserVoucher>();
        }

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? GoogleId { get; set; }
        public string? Dob { get; set; }
        public DateTime CreateAt { get; set; }
        public string? Token { get; set; }
        public Guid RoleId { get; set; }
        public int Status { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }
        public virtual ICollection<UserVoucher> UserVouchers { get; set; }
    }
}
