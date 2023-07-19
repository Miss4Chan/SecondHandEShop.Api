using Domain.Domain_models;
using Domain.DTO;
using System.Collections.Generic;


namespace Service.Interface
{
    public interface IProductService
    {
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProduct(int id);
        ProductDTO EditProduct(ProductDTO productDTO);
        ProductDTO CreateProduct(Product product);
        void DeleteProduct(ProductDTO productDTO);
        public bool AddToShoppingCart(Product product, string email);
    }
}
