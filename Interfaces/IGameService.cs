using SF_API.Common;
using SF_API.DTOs.Game;
using SF_API.Models;

namespace SF_API.Interfaces
{
    public interface IGameService : IRepository<Game>
    {
        Task<ServiceResult<Game>> AddAsync(CreateGameDTO entity);
        Task<ServiceResult<Game>> UpdateAsync(int id, CreateGameDTO entity);

    }
}
