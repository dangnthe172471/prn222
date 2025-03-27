using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Status { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
