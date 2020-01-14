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

        public async Task AddReactieAsync(Reactie reactie)
        {
            await _context.Reactie.AddAsync(reactie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"creating the reaction did not succeed");
            }
        }

        public async Task<IEnumerable<BeerDbUser>> GetAllGebruikers()
        {
            return await _context.BeerDbUser.OrderBy(q => q.Id).AsNoTracking().ToListAsync();
        }

        public async Task AddGebruiker(BeerDbUser beerDbUser)
        {
            await _context.BeerDbUser.AddAsync(beerDbUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception($"creating the user {beerDbUser.UserName} did not succeed");
            }
        }
    }
}
