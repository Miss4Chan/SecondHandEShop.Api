using Domain.Domain_models;
using System;

namespace Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }

        public static explicit operator ProductDTO(Product p) => new ProductDTO
        {
            Id = p.Id,
            ProductDescription = p.ProductDescription,
            ProductName = p.ProductName
        };
    }
}
