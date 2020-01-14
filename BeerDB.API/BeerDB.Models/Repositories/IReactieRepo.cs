using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public interface IReactieRepo
    {
        Task<IEnumerable<Reactie>> GetAllAsync();
        Task AddReactieAsync(Reactie reactie);
        Task AddGebruiker(BeerDbUser beerDbUser);
        Task<IEnumerable<BeerDbUser>> GetAllGebruikers();
    }
}