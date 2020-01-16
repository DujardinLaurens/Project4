using BeerDB.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public class AuthenticatieRepo : IAuthenticatieRepo
    {
        private BeerDBAPIContext _context;

        public AuthenticatieRepo(BeerDBAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDbUser>> GetAllUser()
        {
            return await _context.BeerDbUser.OrderBy(q => q.Id).AsNoTracking().ToListAsync();
        }


    }
}
