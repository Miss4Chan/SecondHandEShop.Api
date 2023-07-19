using Domain.Domain_models;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandEShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this._shoppingCartService = shoppingCartService;
        }

        [HttpGet]
            public IActionResult GetShoppingCart(string email)
            {
                return Ok(_shoppingCartService.getShoppingCartInfo(email));
            }

        [HttpDelete]
            public IActionResult DeleteFromShoppingCart(string email, int product)
            {

            return Ok(_shoppingCartService.deleteProductFromShoppingCart(email, product));
            }
    }
}
