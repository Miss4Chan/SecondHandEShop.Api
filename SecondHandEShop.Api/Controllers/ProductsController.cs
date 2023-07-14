using Domain.Domain_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandEShop.Api.Controllers
{
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
            return CreatedAtRoute("GetExpense", new { newProduct.Id }, newProduct);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Product product)
        {
            _productService.DeleteProduct(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult EditProduct(Product product)
        {
            return Ok(_productService.EditProduct(product));
        }
    }
}
