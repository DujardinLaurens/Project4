using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerDB.Models.Repositories
{
    public interface ICommentRepo
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetBySelectedId(string selectId);
        Task AddAsync(Comment reactie);
    }
}