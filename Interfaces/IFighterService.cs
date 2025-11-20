using SF_API.Common;
using SF_API.DTOs.Fighter;
using SF_API.Models;

namespace SF_API.Interfaces
{
    public interface IFighterService : IRepository<Fighter>
    {
        Task<ServiceResult<Fighter>> AddAsync(CreateFighterDTO entity);
        Task<ServiceResult<Fighter>> UpdateAsync(int id, CreateFighterDTO entity);

    }
}
