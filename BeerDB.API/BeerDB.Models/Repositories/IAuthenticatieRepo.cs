using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public interface IAuthenticatieRepo
    {
        Task<IEnumerable<BeerDbUser>> GetAllUser();
    }
}