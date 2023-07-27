using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandEShop.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserProfileService _userService;

        public UserController(IUserProfileService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult GetMyProfile()
        {
            return Ok(_userService.GetMyProfile());
        }

        [HttpGet("{username}")]
        public IActionResult GetProfile(string username)
        {
            return Ok(_userService.GetProfile(username));
        }

    }
}
