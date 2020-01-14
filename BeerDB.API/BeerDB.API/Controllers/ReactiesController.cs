using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerDB.API.Models;
using BeerDB.Models;
using BeerDB.Models.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BeerDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactiesController : ControllerBase
    {
        private readonly BeerDBAPIContext _context;
        private readonly IReactieRepo _reactieRepo;
        private readonly ILogger<ReactiesController> _logger;

        public ReactiesController(BeerDBAPIContext context, IReactieRepo reactieRepo, ILogger<ReactiesController> logger)
        {
            _context = context;
            _reactieRepo = reactieRepo;
            _logger = logger;
        }

        // GET: api/Reacties
        [HttpGet]
        public async Task<IEnumerable<Reactie>> GetAllReacties()
        {
            return await _reactieRepo.GetAllAsync();
        }

        // GET: api/Reacties/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reactie = await _context.Reactie.FindAsync(id);

            if (reactie == null)
            {
                return NotFound();
            }

            return Ok(reactie);
        }

        // PUT: api/Reacties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactie([FromRoute] int id, [FromBody] Reactie reactie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reactie.Id)
            {
                return BadRequest();
            }

            _context.Entry(reactie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reacties
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostReactie([FromBody] Reactie reactie)
        {
            try
            {
                reactie.timePosted = DateTime.UtcNow;
                reactie.gebruiker = User.Identity.Name;
                await _reactieRepo.AddReactieAsync(reactie);

                return CreatedAtAction("GetAllReacties", new { id = reactie.Id }, reactie);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown when trying to create a new quiz: {ex}");
            }
            return BadRequest("Invalid data to create a new quiz");
        }

        // DELETE: api/Reacties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReactie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reactie = await _context.Reactie.FindAsync(id);
            if (reactie == null)
            {
                return NotFound();
            }

            _context.Reactie.Remove(reactie);
            await _context.SaveChangesAsync();

            return Ok(reactie);
        }

        private bool ReactieExists(int id)
        {
            return _context.Reactie.Any(e => e.Id == id);
        }
    }
}