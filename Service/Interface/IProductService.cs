using Domain.Domain_models;
using Domain.DTO;
using System.Collections.Generic;


namespace Service.Interface
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts(string searchTerm, string colorFilter, string sizeFilter, string conditionFilter, string sortByPrice);
        List<ProductDTO> GetMyProducts();
        ProductDTO GetProduct(int id);
        ProductDTO EditProduct(ProductDTO productDTO);
        ProductDTO CreateProduct(Product product);
        void DeleteProduct(ProductDTO productDTO);
        bool AddToShoppingCart(Product product, string email);
        bool AddToFavourites(Product product, string email);
    }
}
