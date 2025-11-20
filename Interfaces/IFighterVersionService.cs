using SF_API.Common;
using SF_API.DTOs.FighterVersion;
using SF_API.Models;

namespace SF_API.Interfaces
{
    public interface IFighterVersionService : IRepository<FighterVersion>
    {
        Task<ServiceResult<FighterVersion>> AddAsync(CreateFighterVersionDTO entity);
        Task<ServiceResult<FighterVersion>> UpdateAsync(int id, UpdateFighterVersionDTO entity);
    }
}
