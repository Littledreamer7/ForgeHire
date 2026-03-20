using ForgeHire.Models;

namespace ForgeHire.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByMobile(string mobile);
        Task AddAsync(User user);
    }
}
