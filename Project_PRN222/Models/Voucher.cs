using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            UserVouchers = new HashSet<UserVoucher>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public float Discount { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }
        public int Status { get; set; }

        public virtual ICollection<UserVoucher> UserVouchers { get; set; }
    }
}
