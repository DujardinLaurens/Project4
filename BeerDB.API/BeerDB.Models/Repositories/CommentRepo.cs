using BeerDB.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private BeerDBAPIContext _context;

        public CommentRepo(BeerDBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comment.OrderBy(r => r.Id).AsNoTracking().ToListAsync();
        }


        public async Task AddAsync(Comment reactie)
        {
            await _context.Comment.AddAsync(reactie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"creating the reaction did not succeed, {ex}");
            }
        }

        public async Task<IEnumerable<Comment>> GetBySelectedId(string selectId)
        {
            return await _context.Comment.Where(c => c.selectedId == selectId).AsNoTracking().ToListAsync();
        }
    }
}
