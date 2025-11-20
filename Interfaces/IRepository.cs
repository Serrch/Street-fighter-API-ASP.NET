using SF_API.Common;

namespace SF_API.Interfaces
{
    public interface IRepository<T>
    {
        Task<ServiceResult<List<T>>> GetAllAsync();

        Task<ServiceResult<T>> GetByIdAsync(int id);

        Task<ServiceResult<T>> DeleteAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
    }
}
