using BeerDB.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public class ReactieRepo : IReactieRepo
    {
        private BeerDBAPIContext _context;
        public ReactieRepo(BeerDBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reactie>> GetAllAsync()
        {
            return await _context.Reactie.OrderBy(r => r.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Reactie> AddReactieAsync(Reactie reactie)
        {
            await _context.Reactie.AddAsync(reactie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception($"creating the reaction did not succeed");
            }
            return reactie;
        }
    }
}
