﻿using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ShopApplicationUser User { get; set; }
        public virtual ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}
