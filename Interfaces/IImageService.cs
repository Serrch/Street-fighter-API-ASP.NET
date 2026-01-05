using SF_API.Common;
using SF_API.DTOs.Image;
using SF_API.Models;

namespace SF_API.Interfaces
{
    public interface IImageService
    {
        Task<ServiceResult<Image>> GetByIdAsync(int id);

        Task<ServiceResult<Image>> AddAsync(CreateImageDTO entity);

        Task<ServiceResult<Image>> UpdateAsync(int id, CreateImageDTO entity);

        Task<ServiceResult<bool>> DeleteByIdAsync(int id);
    }
}
