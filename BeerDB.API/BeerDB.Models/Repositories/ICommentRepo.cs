using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public interface ICommentRepo
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task AddAsync(Comment reactie);
    }
}