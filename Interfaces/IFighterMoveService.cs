using SF_API.Common;
using SF_API.DTOs.FighterMove;
using SF_API.Models;

namespace SF_API.Interfaces
{
    public interface IFighterMoveService : IRepository<FighterMove>
    {
        Task<ServiceResult<FighterMove>> AddAsync(CreateFighterMoveDTO entity);
        Task<ServiceResult<FighterMove>> UpdateAsync(int id, UpdateFighterMoveDTO entity);
    }
}
