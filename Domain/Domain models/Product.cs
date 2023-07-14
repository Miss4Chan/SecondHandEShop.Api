using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Product : BaseEntity
    {
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }

        [ForeignKey("ShopApplicationUserId")]
        public ShopApplicationUser ShopApplicationUser { get; set; }

    }
}
