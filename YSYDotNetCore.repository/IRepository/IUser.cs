using YSYDotNetCore.repository.Models;

namespace YSYDotNetCore.repository.IRepository
{
    public interface IUser
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task AddAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(int id);
    }
}
