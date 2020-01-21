using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BeerDB.API.Models;
using BeerDB.API.Services;
using BeerDB.Models;
using BeerDB.Models.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BeerDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BeerDBAPIContext _context;
        private readonly IAuthenticatieRepo _authenticatieRepo;
        private readonly SignInManager<BeerDbUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<BeerDbUser> _userManager;
        private IPasswordHasher<BeerDbUser> _hasher;

        public AuthController(BeerDBAPIContext context, IAuthenticatieRepo authenticatieRepo, RoleManager<IdentityRole> roleManager, SignInManager<BeerDbUser> signInManager, ILogger<AuthController> logger, UserManager<BeerDbUser> userManager, IPasswordHasher<BeerDbUser> hasher, IConfiguration configuration)
        {
            _context = context;
            _authenticatieRepo = authenticatieRepo;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            _hasher = hasher;
            _configuration = configuration;
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
                    return Ok(identityModel.UserName);
                }
                ModelState.AddModelError("", "Username or password not found");
                return BadRequest("Failed to login");
                //zo algemeen mogelijke reactive. Vertelt niet dat het pwd niet juist is.
            }
            catch (Exception exc)
            {
                _logger.LogError("exception thrown when logging in: " + exc);
            }
            return BadRequest("Failed to login"); //zo weinig mogelijk (hacker) info
        }

        // GET: api/Auth/gebruikers
        [HttpGet("gebruikers")]
        public async Task<IEnumerable<BeerDbUser>> GetUsers()
        {
            return await _authenticatieRepo.GetAllUser();
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser([FromBody] BeerDbUser beerDbUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("unvalid data");
            try
            {
                var user = await _userManager.FindByNameAsync(beerDbUser.UserName);

                // Add User
                if (user == null)
                {
                    if (!(await _roleManager.RoleExistsAsync("Admin")))
                    {
                        var role = new IdentityRole("Admin");
                        await _roleManager.CreateAsync(role);
                    }

                    user = new BeerDbUser()
                    {
                        UserName = beerDbUser.UserName,
                        SecurityStamp = Guid.NewGuid().ToString()

                    };

                    var userResult = await _userManager.CreateAsync(user, beerDbUser.Password);
                    var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

                    if (!userResult.Succeeded || !roleResult.Succeeded)
                    {
                        throw new InvalidOperationException("Username already taken");
                    }
                    return CreatedAtAction("GetUsers", beerDbUser);
                }
            }
            catch (Exception exc)
            {
                throw new InvalidOperationException("Password not strong enough");
                //this._logger.LogError($"Exception thrown when trying to register in: {exc}");
            }
            throw new InvalidOperationException("Username already exists"); //zo weinig mogelijk (hacker) info 
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            if (!ModelState.IsValid)
                return BadRequest("unvalid data");
            try
            {
                await _signInManager.SignOutAsync();
                return StatusCode(200, "logged out");
            }
            catch (Exception exc)
            {
                this._logger.LogError($"Exception thrown when trying to register in: {exc}");
            }
            return BadRequest("Logout didn't work"); //zo weinig mogelijk (hacker) info 
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