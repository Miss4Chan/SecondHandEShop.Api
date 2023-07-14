using Domain.Domain_models;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;


namespace SecondHandEShop.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetAllProducts());
        }
        [HttpGet("{id}",Name ="GetProduct")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productService.GetProduct(id));
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var newProduct = _productService.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { newProduct.Id }, newProduct);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(ProductDTO productDTO)
        {
            _productService.DeleteProduct(productDTO);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditProduct(ProductDTO productDTO)
        {
            return Ok(_productService.EditProduct(productDTO));
        }
    }
}
