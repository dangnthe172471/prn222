﻿using System;
using System.Collections.Generic;

namespace Project_PRN221.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
