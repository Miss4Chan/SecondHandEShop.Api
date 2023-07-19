using Domain.Domain_models;

namespace Domain.DTO
{
    public class AddProductToShoppingCartDTO
    {
        public Product Product { get; set; }
        public string Email { get; set; }
    }
}
