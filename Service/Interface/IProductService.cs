using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        Product EditProduct(Product product);
        Product CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
