using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebJWTCustomKey.App_Code;
using WebJWTCustomKey.Models;

namespace WebJWTCustomKey.Controllers
{
    [ApiController]
    public class PlanController : Controller
    {
        private IUserService _userService;

        public PlanController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllS")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
