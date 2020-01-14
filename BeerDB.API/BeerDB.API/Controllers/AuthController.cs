using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerDB.API.Models;
using BeerDB.API.Services;
using BeerDB.Models;
using BeerDB.Models.Repositories;
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
        private readonly IReactieRepo _reactieRepo;
        private readonly SignInManager<BeerDbUser> _signInManager;
        private readonly ILogger<AuthController> _logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly UserManager<BeerDbUser> _userManager;
        private IPasswordHasher<BeerDbUser> _hasher;

        public AuthController(BeerDBAPIContext context, IReactieRepo reactieRepo, SignInManager<BeerDbUser> signInManager, ILogger<AuthController> logger, Microsoft.Extensions.Configuration.IConfiguration configuration, UserManager<BeerDbUser> userManager, IPasswordHasher<BeerDbUser> hasher)
        {
            _context = context;
            _reactieRepo = reactieRepo;
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

        // GET: api/Auth/gebruikers
        [HttpGet("gebruikers")]
        public async Task<IEnumerable<BeerDbUser>> GetGebruikers()
        {
            return await _reactieRepo.GetAllGebruikers();
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> PostGebruiker([FromBody] BeerDbUser beerDbUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("unvalid data");
            try
            {
                if (_userManager.FindByNameAsync(beerDbUser.UserName)
                {
                    this._logger.LogError($"Username already exists");
                }
                await _reactieRepo.AddGebruiker(beerDbUser);

                return CreatedAtAction("GetGebruikers", new { id = beerDbUser.Id }, beerDbUser);
            }
            catch (Exception exc)
            {
                this._logger.LogError($"Exception thrown when trying to register in: {exc}");
            }
            return BadRequest("Failed to register"); //zo weinig mogelijk (hacker) info 
        }
    }
}