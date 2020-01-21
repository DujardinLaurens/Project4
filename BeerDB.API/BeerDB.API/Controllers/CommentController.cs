using BeerDB.API.Models;
using BeerDB.Models;
using BeerDB.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BeerDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly BeerDBAPIContext _context;
        private readonly ICommentRepo _commentRepo;
        private readonly ILogger<CommentController> _logger;
        private readonly UserManager<BeerDbUser> _userManager;

        public CommentController(BeerDBAPIContext context, ICommentRepo commentRepo, ILogger<CommentController> logger, UserManager<BeerDbUser> userManager)
        {
            _context = context;
            _commentRepo = commentRepo;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/comment
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _commentRepo.GetAllAsync();
        }

        [HttpGet("{selectId}")]
        [AllowAnonymous]
        public async Task<IEnumerable<Comment>> GetCommentsForSelectedId(string selectId)
        {
            return await _commentRepo.GetBySelectedId(selectId);
        }

        // PUT: api/Reacties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReactie([FromRoute] int id, [FromBody] Comment reactie)
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

        // POST: api/Comment
        [HttpPost]
        [EnableCors("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            try
            {
                comment.timePosted = DateTime.UtcNow;
                await _commentRepo.AddAsync(comment);

                return CreatedAtAction("GetAllComments", comment);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Exception thrown when trying to create a new comment: {ex}");
            }
            return BadRequest("Invalid data to create a new Comment");
        }

        // DELETE: api/Reacties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReactie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reactie = await _context.Comment.FindAsync(id);
            if (reactie == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(reactie);
            await _context.SaveChangesAsync();

            return Ok(reactie);
        }

        private bool ReactieExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}