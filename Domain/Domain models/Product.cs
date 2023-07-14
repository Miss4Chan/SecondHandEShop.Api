using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Domain_models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }

    }
}
