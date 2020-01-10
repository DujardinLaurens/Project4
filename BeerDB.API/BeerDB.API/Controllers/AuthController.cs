using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerDB.API.Models;
using BeerDB.API.Services;
using BeerDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeerDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BeerDBAPIContext _context;
        private readonly SignInManager<BeerDbUser> _signInManager;
        private readonly ILogger<AuthController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly UserManager<BeerDbUser> _userManager;
        private IPasswordHasher<BeerDbUser> _hasher;

        public AuthController(BeerDBAPIContext context, SignInManager<BeerDbUser> signInManager, ILogger<AuthController> logger, Microsoft.Extensions.Configuration.IConfiguration configuration, UserManager<BeerDbUser> userManager, IPasswordHasher<BeerDbUser> hasher)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _hasher = hasher;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] IdentityModel identityModel)
        {
            //LoginViewModel met (Required) UserName en Password aanbrengen.
            if (!ModelState.IsValid)
                return BadRequest("Unvalid data");
            try
            {
                //geen persistence, geen lockout -> via false, false
                var result = await
                _signInManager.PasswordSignInAsync(identityModel.UserName,
                identityModel.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("Welcome " + identityModel.UserName);
                }
                ModelState.AddModelError("", "Username or password not found");
                return BadRequest("Failed to login");
                //zo algemeen mogelijke reactive. Vertelt niet dat het pwd niet juist is.
            }
            catch (Exception exc)
            {
                _logger.LogError($"Exception thrown when logging in: {exc}");
            }
            return BadRequest("Failed to login"); //zo weinig mogelijk (hacker) info
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateJwtToken([FromBody]IdentityModel identityModel)
        {
            try
            {
                var jwtsvc = new JWTServices<BeerDbUser>(_configuration, _logger, _userManager, _hasher);
                var token = await jwtsvc.GenerateJwtToken(identityModel);
                return Ok(token);
            }
            catch (Exception exc)
            {
                _logger.LogError($"Exception thrown when creating JWT: {exc}");
            }
            //Bij niet succesvolle authenticatie wordt een Badrequest (=zo weinig mogelijkeinfo) teruggeven.
            return BadRequest("Failed to generate JWT token");
        }
    }
}