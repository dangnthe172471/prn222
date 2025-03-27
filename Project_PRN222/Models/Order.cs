using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ShipmentDetailsId { get; set; }
        public DateTime CreateAt { get; set; }
        public float TotalPrice { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }

        public virtual ShipmentDetail ShipmentDetails { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
