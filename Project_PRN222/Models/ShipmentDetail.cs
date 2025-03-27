using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class ShipmentDetail
    {
        public ShipmentDetail()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public Guid UserId { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
